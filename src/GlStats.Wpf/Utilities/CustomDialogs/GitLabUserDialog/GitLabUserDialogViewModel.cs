using System.Collections.ObjectModel;
using GlStats.Core.Boundaries.UseCases.SearchUsers;
using GlStats.Core.Entities;
using GlStats.Wpf.Presenters;
using GlStats.Wpf.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Utilities.CustomDialogs.GitLabUserDialog;

public class GitLabUserDialogViewModel : BindableBase
{
    private readonly ISearchUsersUseCase _searchUsersUseCase;
    private readonly SearchUsersPresenter _searchUsersOutput;


    private readonly IDialogCoordinator _dialogCoordinator;

    public static Action ReturnHander;
    public static CustomDialog? Dialog;
    public static User? User;

    public DelegateCommand SearchUsersCommand { get; private set; }
    public DelegateCommand UserSelectedCommand { get; private set; }

    public GitLabUserDialogViewModel(ISearchUsersUseCase searchUsersUseCase, ISearchUsersOutputPort searchUsersOutput, IDialogCoordinator dialogCoordinator)
    {
        _searchUsersUseCase = searchUsersUseCase;
        _searchUsersOutput = (SearchUsersPresenter)searchUsersOutput;

        _dialogCoordinator = dialogCoordinator;

        IsLoading = false;
        Users = new ObservableCollection<User>();

        SearchUsersCommand = new DelegateCommand(SearchUsers);
        UserSelectedCommand = new DelegateCommand(UserSelected);
    }

    async void SearchUsers()
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return;

        try
        {
            IsLoading = true;

            await _searchUsersUseCase.ExecuteAsync(SearchTerm);
            Users.Clear();
            foreach (var user in _searchUsersOutput.Users)
            {
                Users.Add(user);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    void UserSelected()
    {

        User = SelectedUser;

        var metroWindow = Application.Current.MainWindow as MetroWindow;
        metroWindow.HideMetroDialogAsync(Dialog);

        ReturnHander();
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private IList<User> _users;
    public IList<User> Users
    {
        get => _users;
        set => SetProperty(ref _users, value);
    }

    private string _searchTerm;
    public string SearchTerm
    {
        get => _searchTerm;
        set => SetProperty(ref _searchTerm, value);
    }

    private User _selectedUser;

    public User SelectedUser
    {
        get => _selectedUser;
        set => SetProperty(ref _selectedUser, value);
    }
}