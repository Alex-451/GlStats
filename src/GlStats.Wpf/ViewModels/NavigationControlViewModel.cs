using GlStats.Wpf.Views;
using Prism.Regions;

namespace GlStats.Wpf.ViewModels;

public class NavigationControlViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;

    public DelegateCommand OpenTeamsControlCommand { get; private set; }

    public NavigationControlViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;

        OpenTeamsControlCommand = new DelegateCommand(OpenTeamsControl);
    }

    void OpenTeamsControl()
    {
        _regionManager.RegisterViewWithRegion("ContentRegion", typeof(TeamOverviewControl));
    }
}

