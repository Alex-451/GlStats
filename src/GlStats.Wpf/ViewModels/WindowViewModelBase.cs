namespace GlStats.Wpf.ViewModels;

public abstract class WindowViewModelBase : BindableBase
{
    private string _title = string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}