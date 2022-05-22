using System.Resources;
using GlStats.Core.Boundaries.UseCases.GetTeamById;
using GlStats.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class GetTeamByIdPresenter : IGetTeamByIdOutputPort
{
    public bool HasDatabaseConnection = true;
    public Team Team;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public GetTeamByIdPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(Team team)
    {
        Team = team;
    }

    public void NoDatabaseConnection()
    {
        HasDatabaseConnection = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));
    }
}