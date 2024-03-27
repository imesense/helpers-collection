using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace ImeSense.Controls.WinForms.PropertyGrid;

public class PropertyBag : ICustomTypeDescriptor {
    [Serializable]
    public class PropertySpecCollection : IList, ICollection, IEnumerable {
        private readonly ArrayList _innerArray;

        object IList.this[int index] {
            get => this[index];
            set => this[index] = (PropertySpec) value;
        }

        public PropertySpec this[int index] {
            get => (PropertySpec) _innerArray[index];
            set => _innerArray[index] = value;
        }

        public int Count => _innerArray.Count;

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        object ICollection.SyncRoot => null;

        public PropertySpecCollection() {
            _innerArray = new ArrayList();
        }

        int IList.Add(object value) {
            return Add((PropertySpec) value);
        }

        bool IList.Contains(object obj) {
            return Contains((PropertySpec) obj);
        }

        int IList.IndexOf(object obj) {
            return IndexOf((PropertySpec) obj);
        }

        void IList.Insert(int index, object value) {
            Insert(index, (PropertySpec) value);
        }

        void IList.Remove(object value) {
            Remove((PropertySpec) value);
        }

        public int Add(PropertySpec value) {
            return _innerArray.Add(value);
        }

        public void AddRange(PropertySpec[] array) {
            _innerArray.AddRange(array);
        }

        public void Clear() {
            _innerArray.Clear();
        }

        public bool Contains(PropertySpec item) {
            return _innerArray.Contains(item);
        }

        public bool Contains(string name) {
            foreach (PropertySpec item in _innerArray) {
                if (item.Name == name) {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(PropertySpec[] array) {
            _innerArray.CopyTo(array);
        }

        public void CopyTo(PropertySpec[] array, int index) {
            _innerArray.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator() {
            return _innerArray.GetEnumerator();
        }

        public int IndexOf(PropertySpec value) {
            return _innerArray.IndexOf(value);
        }

        public int IndexOf(string name) {
            var num = 0;
            foreach (PropertySpec item in _innerArray) {
                if (item.Name == name) {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public void Insert(int index, PropertySpec value) {
            _innerArray.Insert(index, value);
        }

        public void Remove(PropertySpec obj) {
            _innerArray.Remove(obj);
        }

        public void Remove(string name) {
            var index = IndexOf(name);
            RemoveAt(index);
        }

        public void RemoveAt(int index) {
            _innerArray.RemoveAt(index);
        }

        public PropertySpec[] ToArray() {
            return (PropertySpec[]) _innerArray.ToArray(typeof(PropertySpec));
        }

        void ICollection.CopyTo(Array array, int index) {
            CopyTo((PropertySpec[]) array, index);
        }
    }

    public class PropertySpecDescriptor : PropertyDescriptor {
        public PropertyBag _bag;

        public PropertySpec _item;

        public override Type ComponentType => _item.GetType();

        public override Type PropertyType => Type.GetType(_item.TypeName);

        public override bool IsReadOnly => Attributes.Matches(ReadOnlyAttribute.Yes);

        public PropertySpecDescriptor(
            PropertySpec item, PropertyBag bag, string name,
            Attribute[] attrs
        ) : base(name, attrs) {
            _bag = bag;
            _item = item;
        }

        public override bool CanResetValue(object component) {
            if (_item.DefaultValue == null) {
                return false;
            }
            return !GetValue(component).Equals(_item.DefaultValue);
        }

        public override object GetValue(object component) {
            var propertySpecEventArgs = new PropertySpecEventArgs(_item, null);
            _bag.OnGetValue(propertySpecEventArgs);
            return propertySpecEventArgs.Value;
        }

        public override void ResetValue(object component) {
            SetValue(component, _item.DefaultValue);
        }

        public override void SetValue(object component, object value) {
            var eventArgs = new PropertySpecEventArgs(_item, value);
            _bag.OnSetValue(eventArgs);
        }

        public override bool ShouldSerializeValue(object component) {
            var value = GetValue(component);
            if (_item.DefaultValue == null && value == null) {
                return false;
            }
            return !value.Equals(_item.DefaultValue);
        }
    }

    public string DefaultProperty { get; set; }
    public PropertySpecCollection Properties { get; }

    public event PropertySpecEventHandler GetValue;

    public event PropertySpecEventHandler SetValue;

    public PropertyBag() {
        DefaultProperty = null;
        Properties = new PropertySpecCollection();
    }

    protected virtual void OnGetValue(PropertySpecEventArgs e) {
        GetValue?.Invoke(this, e);
    }

    protected virtual void OnSetValue(PropertySpecEventArgs e) {
        SetValue?.Invoke(this, e);
    }

    AttributeCollection ICustomTypeDescriptor.GetAttributes() {
        return TypeDescriptor.GetAttributes(this, true);
    }

    string ICustomTypeDescriptor.GetClassName() {
        return TypeDescriptor.GetClassName(this, true);
    }

    string ICustomTypeDescriptor.GetComponentName() {
        return TypeDescriptor.GetComponentName(this, true);
    }

    TypeConverter ICustomTypeDescriptor.GetConverter() {
        return TypeDescriptor.GetConverter(this, true);
    }

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() {
        return TypeDescriptor.GetDefaultEvent(this, true);
    }

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() {
        PropertySpec propertySpec = null;
        if (DefaultProperty != null) {
            var index = Properties.IndexOf(DefaultProperty);
            propertySpec = Properties[index];
        }
        if (propertySpec != null) {
            return new PropertySpecDescriptor(propertySpec, this, propertySpec.Name, null);
        }
        return null;
    }

    object ICustomTypeDescriptor.GetEditor(Type editorBaseType) {
        return TypeDescriptor.GetEditor(this, editorBaseType, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents() {
        return TypeDescriptor.GetEvents(this, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) {
        return TypeDescriptor.GetEvents(this, attributes, true);
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() {
        return ((ICustomTypeDescriptor) this).GetProperties(new Attribute[0]);
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) {
        var arrayList = new ArrayList();
        foreach (PropertySpec property in Properties) {
            var arrayList2 = new ArrayList();
            if (property.Category != null) {
                arrayList2.Add(new CategoryAttribute(property.Category));
            }
            if (property.Description != null) {
                arrayList2.Add(new DescriptionAttribute(property.Description));
            }
            if (property.EditorTypeName != null) {
                arrayList2.Add(new EditorAttribute(property.EditorTypeName, typeof(UITypeEditor)));
            }
            if (property.ConverterTypeName != null) {
                arrayList2.Add(new TypeConverterAttribute(property.ConverterTypeName));
            }
            if (property.Attributes != null) {
                arrayList2.AddRange(property.Attributes);
            }

            var attrs = (Attribute[]) arrayList2.ToArray(typeof(Attribute));
            var value = new PropertySpecDescriptor(property, this, property.Name, attrs);
            arrayList.Add(value);
        }

        var array = (PropertyDescriptor[]) arrayList.ToArray(typeof(PropertyDescriptor));
        return new PropertyDescriptorCollection(array);
    }

    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) {
        return this;
    }
}
