using System;

namespace ImeSense.Controls.WinForms.PropertyGrid;

public class PropertySpec {
    public Attribute[] Attributes { get; set; }

    public string Category { get; set; }

    public string ConverterTypeName { get; set; }

    public object DefaultValue { get; set; }

    public string Description { get; set; }

    public string EditorTypeName { get; set; }

    public string Name { get; set; }

    public string TypeName { get; set; }

    public PropertySpec(
        string name, string type, string category,
        string description, object defaultValue
    ) {
        Name = name;
        TypeName = type;
        Category = category;
        Description = description;
        DefaultValue = defaultValue;
        Attributes = null;
    }

    public PropertySpec(
        string name, string type, string category,
        string description, object defaultValue, string editor,
        string typeConverter
    ) : this(
        name, type, category, description, defaultValue
    ) {
        EditorTypeName = editor;
        ConverterTypeName = typeConverter;
    }

    public PropertySpec(
        string name, string type
    ) : this(
        name, type, null, null, null
    ) {
    }

    public PropertySpec(
        string name, Type type
    ) : this(
        name, type.AssemblyQualifiedName, null, null, null
    ) {
    }

    public PropertySpec(
        string name, string type, string category
    ) : this(
        name, type, category, null, null
    ) {
    }

    public PropertySpec(
        string name, Type type, string category
    ) : this(
        name, type.AssemblyQualifiedName, category, null, null
    ) {
    }

    public PropertySpec(
        string name, string type, string category, string description
    ) : this(
        name, type, category, description, null
    ) {
    }

    public PropertySpec(
        string name, Type type, string category, string description
    ) : this(
        name, type.AssemblyQualifiedName, category, description, null
    ) {
    }

    public PropertySpec(
        string name, Type type, string category, string description,
        object defaultValue
    ) : this(
        name, type.AssemblyQualifiedName, category, description, defaultValue
    ) {
    }

    public PropertySpec(
        string name, Type type, string category, string description,
        object defaultValue, string editor, string typeConverter
    ) : this(
        name, type.AssemblyQualifiedName, category, description, defaultValue,
        editor, typeConverter
    ) {
    }

    public PropertySpec(
        string name, string type, string category, string description,
        object defaultValue, Type editor, string typeConverter
    ) : this(
        name, type, category, description, defaultValue, editor.AssemblyQualifiedName,
        typeConverter
    ) {
    }

    public PropertySpec(
        string name, Type type, string category, string description,
        object defaultValue, Type editor, string typeConverter
    ) : this(
        name, type.AssemblyQualifiedName, category, description, defaultValue,
        editor.AssemblyQualifiedName, typeConverter
    ) {
    }

    public PropertySpec(
        string name, string type, string category, string description,
        object defaultValue, string editor, Type typeConverter
    ) : this(
        name, type, category, description, defaultValue, editor,
        typeConverter.AssemblyQualifiedName
    ) {
    }

    public PropertySpec(
        string name, Type type, string category, string description,
        object defaultValue, string editor, Type typeConverter
    ) : this(
        name, type.AssemblyQualifiedName, category, description, defaultValue,
        editor, typeConverter.AssemblyQualifiedName
    ) {
    }

    public PropertySpec(
        string name, string type, string category, string description,
        object defaultValue, Type editor, Type typeConverter
    ) : this(
        name, type, category, description, defaultValue,
        editor.AssemblyQualifiedName, typeConverter.AssemblyQualifiedName
    ) {
    }

    public PropertySpec(
        string name, Type type, string category, string description,
        object defaultValue, Type editor, Type typeConverter
    ) : this(
        name, type.AssemblyQualifiedName, category, description, defaultValue,
        editor.AssemblyQualifiedName, typeConverter.AssemblyQualifiedName
    ) {
    }
}
