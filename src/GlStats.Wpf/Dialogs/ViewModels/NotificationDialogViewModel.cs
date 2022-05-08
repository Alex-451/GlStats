using GlStats.Wpf.ViewModels;
using Prism.Services.Dialogs;

namespace GlStats.Wpf.Dialogs.ViewModels;

public class NotificationDialogViewModel : ViewModelBase, IDialogAware
{
    public DelegateCommand<string> CloseDialogCommand { get; private set; }

    public NotificationDialogViewModel()
    {
        Title = "Notification";

        CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
    }


    private string _message = string.Empty;
    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value); 
    }

    public event Action<IDialogResult> RequestClose;

    protected virtual void CloseDialog(string parameter)
    {
        ButtonResult result = ButtonResult.None;

        if (parameter?.ToLower() == "true")
            result = ButtonResult.OK;
        else if (parameter?.ToLower() == "false")
            result = ButtonResult.Cancel;

        RaiseRequestClose(new DialogResult(result));
    }

    public virtual void RaiseRequestClose(IDialogResult dialogResult)
    {
        RequestClose?.Invoke(dialogResult);
    }

    public bool CanCloseDialog()
    {
        return true;
    }

    public void OnDialogClosed()
    {
        
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        Message = parameters.GetValue<string>("message");
        Title = parameters.GetValue<string>("title");
    }
}