using System.ComponentModel;
using ToolKIT.Extensions;

namespace ToolKIT.PropertyGrid;

public class PropertyValueVM : BaseVM, IDisposable
{
    protected PropertyDescriptor m_propertyDescriptor;
    protected object m_owner;

    private readonly object? m_defaultValue;

    public PropertyValueVM(PropertyDescriptor propertyDescriptor, object owner)
    {
        m_propertyDescriptor = propertyDescriptor.ThrowIfNull();
        m_owner = owner.ThrowIfNull();

        DefaultValueAttribute? defaultValueAttribute = m_propertyDescriptor.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault();
        if (defaultValueAttribute != null)
        {
            m_defaultValue = defaultValueAttribute.Value;
        }
        else
        {
            m_defaultValue = m_propertyDescriptor.PropertyType.IsValueType ? Activator.CreateInstance(propertyDescriptor.PropertyType) : null;
        }

        m_propertyDescriptor.AddValueChanged(m_owner, OnValueChanged);
    }

    public object? ObjectValue
    {
        get => m_propertyDescriptor.GetValue(m_owner);
        set
        {
            if (ObjectValue != value)
            {
                m_propertyDescriptor.SetValue(m_owner, value);
                NotifyPropertyChanged(nameof(ValueSource));
            }
        }
    }

    public Type PropertyType => m_propertyDescriptor.PropertyType;

    public virtual bool IsReadonly => m_propertyDescriptor.IsReadOnly;

    public virtual bool ValueSource => object.Equals(ObjectValue, m_defaultValue);

    public virtual string Name => m_propertyDescriptor.Name;

    public void Dispose()
    {
        m_propertyDescriptor.RemoveValueChanged(m_owner, OnValueChanged);
        GC.SuppressFinalize(this);
    }

    protected virtual void OnValueChanged(object? sender, EventArgs e)
    {
    }
}
