using System;

namespace ImeSense.Controls.WinForms.PropertyGrid;

public class PropertySpecEventArgs : EventArgs {
    private PropertySpec property;
    public PropertySpec Property {
        get {
            return property;
        }
    }

    private object val;
    public object Value {
        get {
            return val;
        }
        set {
            val = value;
        }
    }

    public PropertySpecEventArgs(PropertySpec property, object val) {
        this.property = property;
        this.val = val;
    }
}
