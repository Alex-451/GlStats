using System.Resources;
using GlStats.Core.Boundaries.UseCases.GetTeams;
using GlStats.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class GetTeamsPresenter : IGetTeamsOutputPort
{
    public bool HasDatabaseConnection = true;
    public IEnumerable<Team> Teams;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public GetTeamsPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(IEnumerable<Team> teams)
    {
        if (!teams.Any())
        {
            Teams = new List<Team>();
            return;
        }

        Teams = teams;
    }

    public void NoDatabaseConnection()
    {
        HasDatabaseConnection = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));

    }
}