using System.Net.Http;
using GlStats.ApiWrapper;
using GlStats.Core;
using GlStats.Core.Boundaries.GetCurrentUser;
using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Core.Boundaries.Providers;
using GlStats.Core.UseCases;
using GlStats.DataAccess;
using GlStats.DataAccess.Repositories;
using GlStats.DataAccess.Repositories.Implementations;
using GlStats.Infrastructure;
using GlStats.Infrastructure.Providers;
using GlStats.Wpf.Presenters;
using GlStats.Wpf.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GlStats.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        var mainWindow = Container.Resolve<MainWindow>();
        var registrationWindow = Container.Resolve<RegistrationWindow>();
        var configuration = Container.Resolve<IAuthentication>();

        if (string.IsNullOrWhiteSpace(configuration.GetConfig().GitLabUrl) || string.IsNullOrWhiteSpace(configuration.GetConfig().GitLabUrl))
            return registrationWindow;

        return mainWindow;
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {

        containerRegistry.RegisterSingleton<HttpClient>();
        containerRegistry.Register<INetwork, Network>();
        containerRegistry.Register<IGitLabClient, GitLabClient>();
        containerRegistry.Register<ICurrentUserProvider, CurrentUserProvider>();
        containerRegistry.Register<IGetCurrentUserUseCase, GetCurrentUserUseCase>();
        containerRegistry.RegisterSingleton<IGetCurrentUserOutputPort, CurrentUserPresenter>();
        containerRegistry.Register<IAuthentication, JsonAuthentication>();
        containerRegistry.Register<JsonConfiguration, JsonConfiguration>();
        containerRegistry.RegisterSingleton<ApplicationDbContext>();
        containerRegistry.Register<ITeamRepository, TeamRepository>();
        containerRegistry.Register<IUnitOfWork, UnitOfWork>();

        #region UseCases


        #endregion

        #region Presenters



        #endregion
    }
}

