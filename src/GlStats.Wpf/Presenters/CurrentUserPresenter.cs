using System.Resources;
using GlStats.Core.Boundaries.UseCases.GetCurrentUser;
using GlStats.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Services.Dialogs;

namespace GlStats.Wpf.Presenters;

public class CurrentUserPresenter : IGetCurrentUserOutputPort
{
    public bool HasConnection = true;
    public bool HasValidConfig = true;
    public CurrentUser CurrentUser;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public CurrentUserPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(CurrentUser currentUser)
    {
        if (string.IsNullOrWhiteSpace(currentUser.Id))
        {
            HasValidConfig = false;
            _window.ShowMessageAsync(_resourceManager.GetString("CouldntFindUser"), _resourceManager.GetString("NoUserFoundWithCredentials"));
            return;
        }

        CurrentUser = currentUser;
    }

    public void InvalidConfig()
    {
        HasValidConfig = false;
        _window.ShowMessageAsync(_resourceManager.GetString("InvalidConfig"), _resourceManager.GetString("ConfigContainsError"));
    }

    public void NoConnection()
    {
        HasConnection = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoConnection"), _resourceManager.GetString("NoInternetConnection"));
    }
}