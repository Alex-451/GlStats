using GlStats.Wpf.Utilities;
using GlStats.Wpf.Views;
using Prism.Regions;

namespace GlStats.Wpf.ViewModels;

public class NavigationControlViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;

    public DelegateCommand OpenProjectsCommand { get; private set; }
    public DelegateCommand OpenTeamsCommand { get; private set; }
    public DelegateCommand OpenSettingsCommand { get; private set; }

    public NavigationControlViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;

        OpenProjectsCommand = new DelegateCommand(OpenProjects);
        OpenTeamsCommand = new DelegateCommand(OpenTeams);
        OpenSettingsCommand = new DelegateCommand(OpenSettings);
    }

    void OpenProjects()
    {
        _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(nameof(ProjectsControl), UriKind.Relative));
    }

    void OpenTeams()
    {
        _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(nameof(TeamOverviewControl), UriKind.Relative));
    }

    void OpenSettings()
    {
        _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(nameof(SettingsControl), UriKind.Relative));
    }
}

