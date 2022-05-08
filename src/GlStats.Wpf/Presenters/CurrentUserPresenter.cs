using GlStats.Core.Boundaries.GetCurrentUser;
using GlStats.Core.Entities;
using Prism.Services.Dialogs;

namespace GlStats.Wpf.Presenters;

public class CurrentUserPresenter : IGetCurrentUserOutputPort
{
    private readonly IDialogService _dialogService;

    public bool HasConnection = true;
    public bool HasValidConfig = true;
    public CurrentUser CurrentUser;

    public CurrentUserPresenter(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public void Default(CurrentUser currentUser)
    {
        if (string.IsNullOrWhiteSpace(currentUser.Id))
        {
            HasValidConfig = false;
            ShowPopup("No results", "Couldn't find user.");
            return;
        }

        CurrentUser = currentUser;
    }

    public void InvalidConfig()
    {
        HasValidConfig = false;
        ShowPopup("Invalid configuration", "The provided configuration might contain errors.");
    }

    public void NoConnection()
    {
        HasConnection = false;
        ShowPopup("No connection", "No internet connection.");
    }

    private void ShowPopup(string title, string message)
    => _dialogService.ShowDialog("NotificationDialog", new DialogParameters($"title={title},message={message}"), null);

}