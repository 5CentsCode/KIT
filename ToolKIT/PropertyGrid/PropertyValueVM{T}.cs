using System.ComponentModel;

namespace ToolKIT.PropertyGrid;

public class PropertyValueVM<T> : PropertyValueVM
{
    public PropertyValueVM(PropertyDescriptor propertyDescriptor, object owner)
        : base(propertyDescriptor, owner)
    {
    }

    public T? Value
    {
        get => (T?)ObjectValue;
        set => ObjectValue = value;
    }
}
