using System.Resources;
using GlStats.Core.Boundaries.UseCases.SearchUsers;
using GlStats.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class SearchUsersPresenter : ISearchUsersOutputPort
{
    public bool HasConnection = true;
    public bool HasValidConfig = true;
    public IEnumerable<User> Users;
    private readonly MetroWindow? _window;


    private readonly ResourceManager _resourceManager;

    public SearchUsersPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(IEnumerable<User> users)
    {
        Users = users;
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