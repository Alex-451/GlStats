using System.Collections.ObjectModel;
using System.Resources;
using GlStats.Core.Boundaries.UseCases.AddTeam;
using GlStats.Core.Boundaries.UseCases.GetTeams;
using GlStats.Core.Entities;
using GlStats.Wpf.Presenters;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GlStats.Wpf.ViewModels
{
    public class TeamOverviewControlViewModel : BindableBase
    {
        private readonly IGetTeamsUseCase _getTeamsUseCase;
        private readonly GetTeamsPresenter _getGetTeamsOutput;

        private readonly IAddTeamUseCase _addTeamUseCase;
        private readonly AddTeamPresenter _addTeamOutput;

        private readonly ResourceManager _resourceManager;

        public DelegateCommand OpenAddTeamDialogCommand { get; private set; }
        public DelegateCommand LoadTeamsCommand { get; private set; }

        public TeamOverviewControlViewModel(IGetTeamsUseCase getTeamsUseCase, IGetTeamsOutputPort getTeamsOutput, IAddTeamUseCase addTeamUseCase, IAddTeamOutputPort addTeamOutput, ResourceManager resourceManager)
        {
            _getTeamsUseCase = getTeamsUseCase;
            _getGetTeamsOutput = (GetTeamsPresenter)getTeamsOutput;

            _addTeamUseCase = addTeamUseCase;
            _addTeamOutput = (AddTeamPresenter) addTeamOutput;

            _resourceManager = resourceManager;

            IsLoadingTeams = false;
            Teams = new ObservableCollection<Team>();

            OpenAddTeamDialogCommand = new DelegateCommand(OpenAddTeamDialog);
            LoadTeamsCommand = new DelegateCommand(LoadTeamsAsync);

        }

        async void LoadTeamsAsync()
        {
            try
            {
                IsLoadingTeams = true;

                await _getTeamsUseCase.Execute();
                foreach (var team in _getGetTeamsOutput.Teams)
                {
                    Teams.Add(team);
                }
            }
            finally
            {
                IsLoadingTeams = false;
            }
        }


        async void OpenAddTeamDialog()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var result = await metroWindow.ShowInputAsync(_resourceManager.GetString("TeamConfiguration"), _resourceManager.GetString("Name"));
            if (!string.IsNullOrWhiteSpace(result))
            {
                await _addTeamUseCase.Execute(result);
               Teams.Add(_addTeamOutput.Team);
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
