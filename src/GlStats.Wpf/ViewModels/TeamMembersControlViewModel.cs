using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using GlStats.Core.Boundaries.UseCases.AddMemberToTeam;
using GlStats.Core.Boundaries.UseCases.GetMembersOfTeam;
using GlStats.Core.Boundaries.UseCases.GetTeamById;
using GlStats.Core.Boundaries.UseCases.GetUsersById;
using GlStats.Core.Boundaries.UseCases.RemoveTeamMember;
using GlStats.Core.Entities;
using GlStats.Wpf.Presenters;
using GlStats.Wpf.Utilities.CustomDialogs.GitLabUserDialog;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;

namespace GlStats.Wpf.ViewModels;

public class TeamMembersControlViewModel : BindableBase, INavigationAware
{
    private readonly IGetTeamByIdUseCase _getTeamByIdUseCase;
    private readonly GetTeamByIdPresenter _getTeamByIdOutput;

    private readonly IGetMembersOfTeamUseCase _getMembersOfTeamUseCase;
    private readonly GetMembersOfTeamPresenter _getMembersOfTeamOutput;
    
    private readonly IAddMemberToTeamUseCase _addMemberToTeamUseCase;
    private readonly AddMemberToTeamPresenter _addMemberToTeamOutput;

    private readonly IGetUsersByIdsUseCase _getUsersByIdsUseCase;
    private readonly GetUsersByIdsPresenter _getUsersByIdsOutput;

    private readonly IRemoveTeamMemberUseCase _removeTeamMemberUseCase;
    private readonly RemoveTeamMemberPresenter _removeTeamMemberOutput;

    private readonly ResourceManager _resourceManager;

    public DelegateCommand AddMemberCommand { get; private set; }
    public DelegateCommand<User> RemoveMemberCommand { get; private set; }

    public TeamMembersControlViewModel(IGetTeamByIdUseCase getTeamByIdUseCase, IGetTeamByIdOutputPort getTeamByIdOutput, IGetMembersOfTeamUseCase getMembersOfTeamUseCase, IGetMembersOfTeamOutputPort getMembersOfTeamOutput, IAddMemberToTeamUseCase addMemberToTeamUse, IAddMemberToTeamOutputPort addMemberToTeamOutput, IGetUsersByIdsUseCase getUsersByIdsUseCase, IGetUsersByIdsOutputPort getUsersByIdsOutput, IRemoveTeamMemberUseCase removeTeamMemberUseCase, IRemoveTeamMemberOutputPort removeTeamMemberOutput, ResourceManager resourceManager)
    {
        _getTeamByIdUseCase = getTeamByIdUseCase;
        _getTeamByIdOutput = (GetTeamByIdPresenter)getTeamByIdOutput;

        _getMembersOfTeamUseCase = getMembersOfTeamUseCase;
        _getMembersOfTeamOutput = (GetMembersOfTeamPresenter)getMembersOfTeamOutput;

        _addMemberToTeamUseCase = addMemberToTeamUse;
        _addMemberToTeamOutput = (AddMemberToTeamPresenter)addMemberToTeamOutput;

        _getUsersByIdsUseCase = getUsersByIdsUseCase;
        _getUsersByIdsOutput = (GetUsersByIdsPresenter)getUsersByIdsOutput;

        _removeTeamMemberUseCase = removeTeamMemberUseCase;
        _removeTeamMemberOutput = (RemoveTeamMemberPresenter) removeTeamMemberOutput;

        _resourceManager = resourceManager;

        IsLoadingTeamMembers = false;
        TeamMembers = new ObservableCollection<User>();

        AddMemberCommand = new DelegateCommand(AddMember);
        RemoveMemberCommand = new DelegateCommand<User>(RemoveMember);
    }

    async void AddMember()
    {
        var metroWindow = (Application.Current.MainWindow as MetroWindow);

        var customDialog = new CustomDialog { Title = $"{_resourceManager.GetString("AddMember")} to {Team.Name}" };
        customDialog.Content = new GitLabUserDialog();

        await metroWindow.ShowMetroDialogAsync(customDialog);
        GitLabUserDialogViewModel.Dialog = customDialog;
        GitLabUserDialogViewModel.ReturnHander = () => { UserSelected(); };
    }

    async void RemoveMember(User user)
    {
        if (!string.IsNullOrWhiteSpace(user.Id))
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var dialogSettings = new MetroDialogSettings
            {
                AffirmativeButtonText = _resourceManager.GetString("Remove"),
                NegativeButtonText = _resourceManager.GetString("Cancel"),

            };
            var result = await metroWindow.ShowMessageAsync(_resourceManager.GetString("RemoveMember"), _resourceManager.GetString("AreYouSureYouWantToRemove") + $" {user.Name} from {Team.Name}?", style: MessageDialogStyle.AffirmativeAndNegative, settings: dialogSettings);

            if (result == MessageDialogResult.Affirmative)
            {
                _removeTeamMemberUseCase.Execute(Team.Id, user.Id);

                if (_removeTeamMemberOutput.RemovedMember)
                    TeamMembers.Remove(TeamMembers.Single(x => x.Id == user.Id));
            }
        }
    }

    void UserSelected()
    {
        if (GitLabUserDialogViewModel.User != null)
        {
            _addMemberToTeamUseCase.Execute(Team.Id, GitLabUserDialogViewModel.User.Id);
            RefreshCollection();
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        var id = navigationContext.Parameters["ID"];
        return Team.Id.Equals(id);
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        var idAsObj = navigationContext.Parameters["ID"];

        if (int.TryParse(idAsObj.ToString(), out int id))
        {
            _getTeamByIdUseCase.Execute(id);
            Team = _getTeamByIdOutput.Team;

            RefreshCollection();
        }
    }

    private async void RefreshCollection()
    {
        try
        {
            IsLoadingTeamMembers = true;

            _getMembersOfTeamUseCase.Execute(Team.Id);
            TeamMembers.Clear();

            List<string> ids = new List<string>();
            foreach (var teamMember in _getMembersOfTeamOutput.TeamMembers)
            {
                ids.Add(teamMember.MemberId);
            }


            if (ids.Count > 0)
            {
                await _getUsersByIdsUseCase.ExecuteAsync(ids.ToArray());

                foreach (var user in _getUsersByIdsOutput.Users)
                {
                    TeamMembers.Add(user);
                }
            }
        }
        finally
        {
            IsLoadingTeamMembers = false;
        }
    }

    private Team _team;
    public Team Team
    {
        get => _team;
        set => SetProperty(ref _team, value);
    }

    private IList<User> _teamMembers;
    public IList<User> TeamMembers
    {
        get => _teamMembers;
        set => SetProperty(ref _teamMembers, value);
    }

    private bool _isLoadingTeamMembers;
    public bool IsLoadingTeamMembers
    {
        get => _isLoadingTeamMembers;
        set => SetProperty(ref _isLoadingTeamMembers, value);
    }
}