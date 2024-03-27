using System.Collections;

namespace ImeSense.Controls.WinForms.PropertyGrid;

public class PropertyTable : PropertyBag {
    private readonly Hashtable _propertyValues;

    public object this[string key] {
        get => _propertyValues[key];
        set => _propertyValues[key] = value;
    }

    public PropertyTable() {
        _propertyValues = new Hashtable();
    }

    protected override void OnGetValue(PropertySpecEventArgs eventArgs) {
        eventArgs.Value = _propertyValues[eventArgs.Property.Name];

        base.OnGetValue(eventArgs);
    }

    protected override void OnSetValue(PropertySpecEventArgs eventArgs) {
        _propertyValues[eventArgs.Property.Name] = eventArgs.Value;

        base.OnSetValue(eventArgs);
    }
}
