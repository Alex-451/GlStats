using GlStats.Wpf.Views;

namespace GlStats.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        var w = Container.Resolve<MainWindow>();
        return w;
    }

    protected override void RegisterTypes(IContainerRegistry container)
    {

    }
}

