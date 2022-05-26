using System.Resources;
using GlStats.Core.Boundaries.UseCases.GetProjects;
using GlStats.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.Presenters;

public class GetProjectsPresenter : IGetProjectsOutputPort
{
    public bool HasConnection = true;
    public bool HasValidConfig = true;
    public IEnumerable<Project> Projects;
    private readonly MetroWindow? _window;

    private readonly ResourceManager _resourceManager;

    public GetProjectsPresenter(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;

        _window = (Application.Current.MainWindow as MetroWindow);
    }

    public void Default(IEnumerable<Project> projects)
    {
        Projects = projects;
    }

    public void InvalidConfig()
    {

        HasValidConfig = false;
        _window.ShowMessageAsync(_resourceManager.GetString("InvalidConfig"), _resourceManager.GetString("ConfigContainsError"));
    }

    public void NoConnection()
    {
        HasConnection = false;
        _window.ShowMessageAsync(_resourceManager.GetString("NoConnection"), _resourceManager.GetString("NoInternetConnection"));
    }
}