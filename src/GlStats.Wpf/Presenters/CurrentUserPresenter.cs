using GlStats.Core.Boundaries.GetCurrentUser;
using GlStats.Core.Entities;
using Prism.Services.Dialogs;

namespace GlStats.Wpf.Presenters;

public class CurrentUserPresenter : IGetCurrentUserOutputPort
{
    public bool HasConnection = true;
    public bool HasValidConfig = true;
    public CurrentUser CurrentUser;

    public void Default(CurrentUser currentUser)
    {
        if (string.IsNullOrWhiteSpace(currentUser.Id))
        {
            HasValidConfig = false;
            MessageBox.Show("Couldn't find user.", "No results");
            return;
        }

        CurrentUser = currentUser;
    }

    public void InvalidConfig()
    {
        HasValidConfig = false;
        MessageBox.Show("The provided configuration might contain errors.", "Invalid configuration");
    }

    public void NoConnection()
    {
        HasConnection = false;
        MessageBox.Show("No internet connection.", "No connection");
    }
}