using System.Resources;
using GlStats.Core.Boundaries.UseCases.RemoveTeamMember;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class RemoveTeamMemberPresenter : IRemoveTeamMemberOutputPort
{
    public bool RemovedMember;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public RemoveTeamMemberPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(bool removed)
    {
        RemovedMember = removed;
    }

    public void NoDatabaseConnection()
    {
        RemovedMember = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoDatabaseConnection"), _resourceManager.GetString("DatabaseConnectionError"));
    }
}