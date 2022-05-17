using System.Net.Http;
using System.Reflection;
using System.Resources;
using GlStats.ApiWrapper;
using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.AddTeam;
using GlStats.Core.Boundaries.UseCases.GetCurrentUser;
using GlStats.Core.Boundaries.UseCases.GetTeams;
using GlStats.Core.UseCases;
using GlStats.DataAccess;
using GlStats.DataAccess.Repositories;
using GlStats.DataAccess.Repositories.Implementations;
using GlStats.Infrastructure;
using GlStats.Infrastructure.Providers;
using GlStats.Wpf.Presenters;
using GlStats.Wpf.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GlStats.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        var mainWindow = Container.Resolve<MainWindow>();
        return mainWindow;
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {

        containerRegistry.RegisterSingleton<HttpClient>();
        containerRegistry.Register<INetwork, Network>();
        containerRegistry.Register<IGitLabClient, GitLabClient>();
        containerRegistry.Register<IAuthentication, JsonAuthentication>();
        containerRegistry.Register<JsonConfiguration, JsonConfiguration>();
        containerRegistry.RegisterSingleton<ApplicationDbContext>();
        containerRegistry.Register<ITeamRepository, TeamRepository>();
        containerRegistry.Register<IUnitOfWork, UnitOfWork>();
        containerRegistry.RegisterInstance(new ResourceManager("GlStats.Wpf.Resources.Strings.AppResource",
            Assembly.GetExecutingAssembly()));


        #region UseCases

        containerRegistry.Register<ICurrentUserProvider, CurrentUserProvider>();
        containerRegistry.Register<IGetCurrentUserUseCase, GetCurrentUserUseCase>();

        containerRegistry.Register<ITeamsProvider, TeamsProvider>();
        containerRegistry.Register<IGetTeamsUseCase, GetTeamsUseCase>();
        containerRegistry.Register<IAddTeamUseCase, AddTeamUseCase>();

        #endregion

        #region Presenters

        containerRegistry.RegisterSingleton<IGetCurrentUserOutputPort, CurrentUserPresenter>();
        containerRegistry.RegisterSingleton<IGetTeamsOutputPort, GetTeamsPresenter>();
        containerRegistry.RegisterSingleton<IAddTeamOutputPort, AddTeamPresenter>();

        #endregion
    }
}

