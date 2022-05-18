using System.Resources;
using GlStats.Core.Boundaries.UseCases.UpdateTeam;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class UpdateTeamPresenter : IUpdateTeamOutputPort
{
    public bool UpdatedEntry = false;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public UpdateTeamPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(bool updated)
    {
       UpdatedEntry = updated;
    }

    public void NoDatabaseConnection()
    {
        UpdatedEntry = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));
    }
}