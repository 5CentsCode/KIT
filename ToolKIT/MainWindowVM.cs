using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using ToolKIT.PropertyGrid;

namespace ToolKIT;

public class MainWindowVM : BaseVM
{
    public MainWindowVM()
    {
        Button = new Button();

        m_properties = new ObservableCollection<object>();
        Properties = new ReadOnlyObservableCollection<object>(m_properties);

        PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(Button);
        foreach (PropertyDescriptor propertyDescriptor in propertyDescriptors)
        {
            Type propertyType = propertyDescriptor.PropertyType;

            if (propertyType == typeof(bool))
            {
                m_properties.Add(new BoolPropertyValueVM(propertyDescriptor, Button));
            }
            else
            {
                m_properties.Add(new PropertyValueVM(propertyDescriptor, Button));
            }
        }
    }

    public Button Button { get; }

    public ReadOnlyObservableCollection<object> Properties { get; }
    private readonly ObservableCollection<object> m_properties;
}
