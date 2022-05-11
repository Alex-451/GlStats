using GlStats.Core.Boundaries.GetCurrentUser;
using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Wpf.Presenters;

namespace GlStats.Wpf.ViewModels;

public class RegistrationWindowViewModel : ViewModelBase
{
    private readonly IGetCurrentUserUseCase _getCurrentUserUseCase;
    private readonly CurrentUserPresenter _output;

    private readonly IAuthentication _auth;

    public DelegateCommand RegisterCommand { get; private set; }

    public RegistrationWindowViewModel(IGetCurrentUserUseCase getCurrentUserUseCase, IGetCurrentUserOutputPort output, IAuthentication auth)
    {
        _getCurrentUserUseCase = getCurrentUserUseCase;
        _output = (CurrentUserPresenter)output;

        _auth = auth;

        RegisterCommand = new DelegateCommand(Register, CanRegister)
            .ObservesProperty(() => Url)
            .ObservesProperty(() => Token);
    }

    async void Register()
    {
        _auth.SetConfig(Url, Token);
        await _getCurrentUserUseCase.Execute();

        if (_output.CurrentUser != null && !string.IsNullOrWhiteSpace(_output.CurrentUser.Id))
        {
            return;
        }

        _auth.SetConfig(string.Empty, string.Empty);
    }

    bool CanRegister()
    {
        if (string.IsNullOrWhiteSpace(Url) || string.IsNullOrWhiteSpace(Token))
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
}