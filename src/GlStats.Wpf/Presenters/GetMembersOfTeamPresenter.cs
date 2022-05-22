using System.Resources;
using GlStats.Core.Boundaries.UseCases.GetMembersOfTeam;
using GlStats.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class GetMembersOfTeamPresenter : IGetMembersOfTeamOutputPort
{
    public bool HasDatabaseConnection = true;
    public IEnumerable<TeamMember> TeamMembers;

    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public GetMembersOfTeamPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(IEnumerable<TeamMember> teamMembers)
    {
        TeamMembers = teamMembers;
    }

    public void NoDatabaseConnection()
    {
        HasDatabaseConnection = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));
    }
}