using System.ComponentModel;

namespace ToolKIT.PropertyGrid;

public class BoolPropertyValueVM : PropertyValueVM<bool>
{
    public BoolPropertyValueVM(PropertyDescriptor propertyDescriptor, object owner)
        : base(propertyDescriptor, owner)
    {
    }
}