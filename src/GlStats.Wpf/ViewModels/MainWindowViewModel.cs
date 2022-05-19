using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Wpf.Views;
using Prism.Regions;

namespace GlStats.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IAuthentication _auth;
        private readonly IRegionManager _regionManager;

        public DelegateCommand RegisterInitViewCommand { get; private set; }

        public MainWindowViewModel(IAuthentication auth, IRegionManager regionManager)
        {
            _auth = auth;
            _regionManager = regionManager;

            RegisterInitViewCommand = new DelegateCommand(RegisterInitialView);
        }

        private void RegisterInitialView()
        {
            if (string.IsNullOrWhiteSpace(_auth.GetConfig().GitLabUrl) ||
                string.IsNullOrWhiteSpace(_auth.GetConfig().GitLabToken))
            {
                _regionManager.RequestNavigate("ContentRegion", new Uri(nameof(RegistrationControl), UriKind.Relative));
            }
            else
            {
                _regionManager.RequestNavigate("NavRegion", new Uri(nameof(NavigationControl), UriKind.Relative));
                _regionManager.RequestNavigate("ContentRegion", new Uri(nameof(StatisticsControl), UriKind.Relative));
            }
        }
    }
}
