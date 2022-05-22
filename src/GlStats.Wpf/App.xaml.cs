using System.Net.Http;
using System.Reflection;
using System.Resources;
using GlStats.ApiWrapper;
using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.AddMemberToTeam;
using GlStats.Core.Boundaries.UseCases.AddTeam;
using GlStats.Core.Boundaries.UseCases.DeleteTeam;
using GlStats.Core.Boundaries.UseCases.GetCurrentUser;
using GlStats.Core.Boundaries.UseCases.GetMembersOfTeam;
using GlStats.Core.Boundaries.UseCases.GetTeamById;
using GlStats.Core.Boundaries.UseCases.GetTeams;
using GlStats.Core.Boundaries.UseCases.SearchUsers;
using GlStats.Core.Boundaries.UseCases.UpdateTeam;
using GlStats.Core.UseCases;
using GlStats.DataAccess;
using GlStats.DataAccess.Repositories;
using GlStats.DataAccess.Repositories.Implementations;
using GlStats.Infrastructure;
using GlStats.Infrastructure.Providers;
using GlStats.Wpf.Presenters;
using GlStats.Wpf.Views;
using LiteDB;
using MahApps.Metro.Controls.Dialogs;
using AddMemberToTeamUseCase = GlStats.Core.UseCases.AddMemberToTeamUseCase;

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
        containerRegistry.Register<IDialogCoordinator, DialogCoordinator>();

        var auth = Container.Resolve<IAuthentication>();
        containerRegistry.RegisterInstance(new LiteDatabase(auth.GetConfig().ConnectionString));

        containerRegistry.Register<IUnitOfWork, UnitOfWork>();
        containerRegistry.RegisterInstance(new ResourceManager("GlStats.Wpf.Resources.Strings.AppResource",
            Assembly.GetExecutingAssembly()));


        #region Navigation

        containerRegistry.RegisterForNavigation(typeof(NavigationControl), nameof(NavigationControl));
        containerRegistry.RegisterForNavigation(typeof(RegistrationControl), nameof(RegistrationControl));
        containerRegistry.RegisterForNavigation(typeof(StatisticsControl), nameof(StatisticsControl));
        containerRegistry.RegisterForNavigation(typeof(TeamOverviewControl), nameof(TeamOverviewControl));
        containerRegistry.RegisterForNavigation(typeof(TeamMembersControl), nameof(TeamMembersControl));

        #endregion

        #region UseCases

        containerRegistry.Register<IGitLabProvider, GitLabProvider>();
        containerRegistry.Register<IGetCurrentUserUseCase, GetCurrentUserUseCase>();
        containerRegistry.Register<ISearchUsersUseCase, SearchUsersUseCase>();

        containerRegistry.Register<ITeamsProvider, TeamsProvider>();
        containerRegistry.Register<IGetTeamsUseCase, GetTeamsUseCase>();
        containerRegistry.Register<IGetTeamByIdUseCase, GetTeamByIdUseCase>();
        containerRegistry.Register<IAddTeamUseCase, AddTeamUseCase>();
        containerRegistry.Register<IUpdateTeamUseCase, UpdateTeamUseCase>();
        containerRegistry.Register<IDeleteTeamUseCase, DeleteTeamUseCase>();

        containerRegistry.Register<ITeamMembersProvider, TeamMembersProvider>();
        containerRegistry.Register<IGetMembersOfTeamUseCase, GetMembersOfTeamUseCase>();
        containerRegistry.Register<IAddMemberToTeamUseCase, AddMemberToTeamUseCase>();


        #endregion

        #region Presenters

        containerRegistry.RegisterSingleton<IGetCurrentUserOutputPort, CurrentUserPresenter>();
        containerRegistry.RegisterSingleton<ISearchUsersOutputPort, SearchUsersPresenter>();

        containerRegistry.RegisterSingleton<IGetTeamsOutputPort, GetTeamsPresenter>();
        containerRegistry.RegisterSingleton<IGetTeamByIdOutputPort, GetTeamByIdPresenter>();
        containerRegistry.RegisterSingleton<IAddTeamOutputPort, AddTeamPresenter>();
        containerRegistry.RegisterSingleton<IUpdateTeamOutputPort, UpdateTeamPresenter>();
        containerRegistry.RegisterSingleton<IDeleteTeamOutputPort, DeleteTeamPresenter>();

        containerRegistry.RegisterSingleton<IGetMembersOfTeamOutputPort, GetMembersOfTeamPresenter>();
        containerRegistry.RegisterSingleton<IAddMemberToTeamOutputPort, AddMemberToTeamPresenter>();

        #endregion

        #region Repositories

        containerRegistry.Register<ITeamRepository, TeamRepository>();
        containerRegistry.Register<ITeamMemberRepository, TeamMemberRepository>();

        #endregion
    }
}

