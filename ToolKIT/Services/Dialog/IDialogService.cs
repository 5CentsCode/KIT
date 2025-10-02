namespace ToolKIT.Services.Dialog;

public interface IDialogService
{
    void Show<T>();

    void Show(Type type);

    void Show(object obj);
}
