using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GlStats.Core.Boundaries.UseCases.AddMemberToTeam;
using GlStats.Core.Boundaries.UseCases.GetMembersOfTeam;
using GlStats.Core.Boundaries.UseCases.GetTeamById;
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

    public DelegateCommand AddMemberCommand { get; private set; }

    public TeamMembersControlViewModel(IGetTeamByIdUseCase getTeamByIdUseCase, IGetTeamByIdOutputPort getTeamByIdOutput, IGetMembersOfTeamUseCase getMembersOfTeamUseCase, IGetMembersOfTeamOutputPort getMembersOfTeamOutput, IAddMemberToTeamUseCase addMemberToTeamUse, IAddMemberToTeamOutputPort addMemberToTeamOutput)
    {
        _getTeamByIdUseCase = getTeamByIdUseCase;
        _getTeamByIdOutput = (GetTeamByIdPresenter)getTeamByIdOutput;

        _getMembersOfTeamUseCase = getMembersOfTeamUseCase;
        _getMembersOfTeamOutput = (GetMembersOfTeamPresenter)getMembersOfTeamOutput;

        _addMemberToTeamUseCase = addMemberToTeamUse;
        _addMemberToTeamOutput = (AddMemberToTeamPresenter)addMemberToTeamOutput;

        TeamMembers = new ObservableCollection<TeamMember>();

        AddMemberCommand = new DelegateCommand(AddMember);
    }

    async void AddMember()
    {
        var metroWindow = (Application.Current.MainWindow as MetroWindow);

        var customDialog = new CustomDialog { Title = "test", };
        customDialog.Content = new GitLabUserDialog();

        await metroWindow.ShowMetroDialogAsync(customDialog);
        GitLabUserDialogViewModel.Dialog = customDialog;
        GitLabUserDialogViewModel.ReturnHander = () => { UserSelected(); };
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

    private void RefreshCollection()
    {
        _getMembersOfTeamUseCase.Execute(Team.Id);
        TeamMembers.Clear();
        foreach (var teamMember in _getMembersOfTeamOutput.TeamMembers)
        {
            TeamMembers.Add(teamMember);
        }
    }

    private Team _team;
    public Team Team
    {
        get => _team;
        set => SetProperty(ref _team, value);
    }

    private IList<TeamMember> _teamMembers;

    public IList<TeamMember> TeamMembers
    {
        get => _teamMembers;
        set => SetProperty(ref _teamMembers, value);
    }
}