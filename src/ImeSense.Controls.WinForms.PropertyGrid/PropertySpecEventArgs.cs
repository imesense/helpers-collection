using System;

namespace ImeSense.Controls.WinForms.PropertyGrid;

public class PropertySpecEventArgs : EventArgs {
    private readonly PropertySpec _property;

    public PropertySpec Property => _property;

    public object Value { get; set; }

    public PropertySpecEventArgs(PropertySpec property, object value) {
        _property = property;

        Value = value;
    }
}
