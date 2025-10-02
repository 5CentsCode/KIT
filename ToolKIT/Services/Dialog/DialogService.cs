
using System.Windows;
using ToolKIT.Extensions;

namespace ToolKIT.Services.Dialog;

public class DialogService : IDialogService
{
    private readonly IServiceProvider m_serviceProvider;

    public DialogService(IServiceProvider serviceProvider)
    {
        m_serviceProvider = serviceProvider.ThrowIfNull();
    }

    public void Show<T>()
    {
        Show(typeof(T));
    }

    public void Show(Type type)
    {
        object? service = m_serviceProvider.GetService(type);
        if (service != null)
        {
            Show(service);
        }
        else
        {
            object? typeInstance = Activator.CreateInstance(type);
            typeInstance.ThrowIfNull();
            Show(typeInstance);
        }
    }

    public void Show(object obj)
    {
        if (obj is Window window)
        {
            window.Show();
        }
        else
        {
            Window newWindow = new Window
            {
                DataContext = obj
            };
            newWindow.Show();
        }
    }
}
