using System.Collections.ObjectModel;
using System.Resources;
using GlStats.Core.Boundaries.UseCases.AddTeam;
using GlStats.Core.Boundaries.UseCases.DeleteTeam;
using GlStats.Core.Boundaries.UseCases.GetTeams;
using GlStats.Core.Boundaries.UseCases.UpdateTeam;
using GlStats.Core.Entities;
using GlStats.Wpf.Presenters;
using GlStats.Wpf.Utilities;
using GlStats.Wpf.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;

namespace GlStats.Wpf.ViewModels
{
    public class TeamOverviewControlViewModel : BindableBase
    {
        private readonly IGetTeamsUseCase _getTeamsUseCase;
        private readonly GetTeamsPresenter _getTeamsOutput;

        private readonly IAddTeamUseCase _addTeamUseCase;
        private readonly AddTeamPresenter _addTeamOutput;

        private readonly IUpdateTeamUseCase _updateTeamUseCase;
        private readonly UpdateTeamPresenter _updateTeamOutput;

        private readonly IDeleteTeamUseCase _deleteTeamUseCase;
        private readonly DeleteTeamPresenter _deleteTeamOutput;

        private readonly ResourceManager _resourceManager;
        private readonly IRegionManager _regionManager;

        public DelegateCommand LoadTeamsCommand { get; private set; }
        public DelegateCommand AddTeamCommand { get; private set; }
        public DelegateCommand<int?> UpdateTeamCommand { get; private set; }
        public DelegateCommand<Team?> DeleteTeamCommand { get; private set; }
        public DelegateCommand<Team?> ManageTeamMembersCommand { get; private set; }

        public TeamOverviewControlViewModel(IGetTeamsUseCase getTeamsUseCase, IGetTeamsOutputPort getTeamsOutput, IAddTeamUseCase addTeamUseCase, IAddTeamOutputPort addTeamOutput, IUpdateTeamUseCase updateTeamUseCase, IUpdateTeamOutputPort updateTeamOutput, IDeleteTeamUseCase deleteTeamUse, IDeleteTeamOutputPort deleteTeamOutput, ResourceManager resourceManager, IRegionManager regionManager)
        {
            _getTeamsUseCase = getTeamsUseCase;
            _getTeamsOutput = (GetTeamsPresenter)getTeamsOutput;

            _addTeamUseCase = addTeamUseCase;
            _addTeamOutput = (AddTeamPresenter)addTeamOutput;

            _updateTeamUseCase = updateTeamUseCase;
            _updateTeamOutput = (UpdateTeamPresenter)updateTeamOutput;

            _deleteTeamUseCase = deleteTeamUse;
            _deleteTeamOutput = (DeleteTeamPresenter)deleteTeamOutput;

            _resourceManager = resourceManager;
            _regionManager = regionManager;

            IsLoadingTeams = false;
            Teams = new ObservableCollection<Team>();

            LoadTeamsCommand = new DelegateCommand(LoadTeams);
            AddTeamCommand = new DelegateCommand(AddTeam);
            UpdateTeamCommand = new DelegateCommand<int?>(UpdateTeam);
            DeleteTeamCommand = new DelegateCommand<Team?>(DeleteTeam);
            ManageTeamMembersCommand = new DelegateCommand<Team?>(ManageTeamMembers);
        }

        void LoadTeams()
        {
            try
            {
                IsLoadingTeams = true;

                RefreshCollection();
            }
            finally
            {
                IsLoadingTeams = false;
            }
        }

        async void AddTeam()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var result = await metroWindow.ShowInputAsync(_resourceManager.GetString("TeamConfiguration"), _resourceManager.GetString("Name"));
            if (!string.IsNullOrWhiteSpace(result))
            {
                _addTeamUseCase.Execute(new Team { Name = result });
                RefreshCollection();
            }
        }

        async void UpdateTeam(int? id)
        {
            if (id == null)
                return;

            var teamToUpdate = Teams.Single(x => x.Id == id);

            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var dialogSettings = new MetroDialogSettings
            {
                AffirmativeButtonText = _resourceManager.GetString("Save"),
                DefaultText = teamToUpdate.Name,
            };
            var result = await metroWindow.ShowInputAsync(_resourceManager.GetString("TeamConfiguration"), _resourceManager.GetString("Name"), dialogSettings);
            if (!string.IsNullOrWhiteSpace(result))
            {
                _updateTeamUseCase.Execute(id.Value, new Team { Name = result });
                if (_updateTeamOutput.UpdatedEntry)
                    RefreshCollection();
            }
        }

        async void DeleteTeam(Team? team)
        {
            if (team != null)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                var dialogSettings = new MetroDialogSettings
                {
                    AffirmativeButtonText = _resourceManager.GetString("Delete"),
                    NegativeButtonText = _resourceManager.GetString("Cancel"),

                };
                var result = await metroWindow.ShowMessageAsync(_resourceManager.GetString("DeleteTeam"), _resourceManager.GetString("AreYouSureYouWantToDelete") + $" {team.Name}?", style: MessageDialogStyle.AffirmativeAndNegative, settings: dialogSettings);

                if (result == MessageDialogResult.Affirmative)
                {
                    _deleteTeamUseCase.Execute(team.Id);

                    if (_deleteTeamOutput.DeletedEntry)
                        Teams.Remove(Teams.Single(x => x.Id == team.Id));
                }
            }
        }

        async void ManageTeamMembers(Team? team)
        {
            if (team != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("ID", team.Id);

                _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(nameof(TeamMembersControl), UriKind.Relative), parameters);
            }
        }

        private void RefreshCollection()
        {
            _getTeamsUseCase.Execute();
            Teams.Clear();
            foreach (var team in _getTeamsOutput.Teams)
            {
                Teams.Add(team);
            }
        }

        private bool _isLoadingTeams;
        public bool IsLoadingTeams
        {
            get => _isLoadingTeams;
            set => SetProperty(ref _isLoadingTeams, value);
        }

        private IList<Team> _teams;
        public IList<Team> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
        }
    }
}
