using System.Resources;
using GlStats.Core.Boundaries.UseCases.DeleteTeam;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class DeleteTeamPresenter : IDeleteTeamOutputPort
{
    public bool DeletedEntry = false;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public DeleteTeamPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(bool deleted)
    {
        DeletedEntry = deleted;
    }

    public void NoDatabaseConnection()
    {
        DeletedEntry = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));
    }
}