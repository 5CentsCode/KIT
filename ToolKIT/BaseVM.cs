using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToolKIT;
public class BaseVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        bool updateValue = EqualityComparer<T>.Default.Equals(field, newValue) == false;
        if (updateValue)
        {
            field = newValue;
            NotifyPropertyChanged(propertyName);
        }

        return updateValue;
    }

    protected void NotifyPropertiesChanged(params string[] propertyNames)
    {
        foreach (string propertyName in propertyNames)
        {
            NotifyPropertyChanged(propertyName);
        }
    }

    protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
