using System.Resources;
using GlStats.Core.Boundaries.UseCases.AddTeam;
using GlStats.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class AddTeamPresenter : IAddTeamOutputPort
{
    public bool HasDatabaseConnection = true;
    public Team Team;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public AddTeamPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(Team team)
    {
        _window.ShowMessageAsync(_resourceManager.GetString("TeamAdded"), _resourceManager.GetString("SuccessfullyCreatedTeam"));
        Team = team;
    }

    public void NoDatabaseConnection()
    {
        HasDatabaseConnection = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));
    }
}