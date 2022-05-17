using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Core.Boundaries.UseCases.GetCurrentUser;
using GlStats.Wpf.Presenters;
using GlStats.Wpf.Views;
using Prism.Regions;

namespace GlStats.Wpf.ViewModels
{
    public class RegistrationControlViewModel : BindableBase
    {
        private readonly IGetCurrentUserUseCase _getCurrentUserUseCase;
        private readonly CurrentUserPresenter _output;

        private readonly IAuthentication _auth;
        private readonly IRegionManager _regionManager;

        public DelegateCommand RegisterCommand { get; private set; }

        public RegistrationControlViewModel(IGetCurrentUserUseCase getCurrentUserUseCase, IGetCurrentUserOutputPort output, IAuthentication auth, IRegionManager regionManager)
        {
            _getCurrentUserUseCase = getCurrentUserUseCase;
            _output = (CurrentUserPresenter)output;

            _auth = auth;
            _regionManager = regionManager;

            IsBusy = false;

            RegisterCommand = new DelegateCommand(Register, CanRegister)
                .ObservesProperty(() => Url)
                .ObservesProperty(() => Token)
                .ObservesProperty(() => IsBusy);
        }

        async void Register()
        {
            try
            {
                IsBusy = true;

                _auth.SetConfig(Url, Token);
                await _getCurrentUserUseCase.Execute();

                if (!string.IsNullOrWhiteSpace(_output.CurrentUser?.Id))
                {
                    return;
                }

                Url = string.Empty;
                Token = string.Empty;
                _auth.SetConfig(string.Empty, string.Empty);

            }
            finally
            {
                IsBusy = false;
            }
            
        }

        bool CanRegister()
        {
            if (string.IsNullOrWhiteSpace(Url) || string.IsNullOrWhiteSpace(Token))
                return false;

            if (IsBusy)
                return false;

            return true;
        }

        private string _url = string.Empty;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        private string _token = string.Empty;
        public string Token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}
