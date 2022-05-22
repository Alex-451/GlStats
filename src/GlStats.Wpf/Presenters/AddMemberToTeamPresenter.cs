using System.Resources;
using GlStats.Core.Boundaries.UseCases.AddMemberToTeam;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class AddMemberToTeamPresenter : IAddMemberToTeamOutputPort
{
    public bool HasDatabaseConnection = true;
    public int TeamMemberId;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public AddMemberToTeamPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(int teamMemberId)
    {
       TeamMemberId = teamMemberId;
    }

    public void NoDatabaseConnection()
    {
        HasDatabaseConnection = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));
    }
}