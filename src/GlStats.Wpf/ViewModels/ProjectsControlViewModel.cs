using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using GlStats.Core.Boundaries.UseCases.GetProjects;
using GlStats.Core.Entities;
using GlStats.Wpf.Presenters;

namespace GlStats.Wpf.ViewModels
{
    public class ProjectsControlViewModel : BindableBase
    {
        private readonly IGetProjectsUseCase _getProjectsUseCase;
        private readonly GetProjectsPresenter _getProjectsOutput;

        private readonly ResourceManager _resourceManager;

        public DelegateCommand LoadProjectsCommand { get; private set; }

        public ProjectsControlViewModel(IGetProjectsUseCase getProjectsUseCase, IGetProjectsOutputPort getProjectsOutput, ResourceManager resourceManager)
        {
            _getProjectsUseCase = getProjectsUseCase;
            _getProjectsOutput = (GetProjectsPresenter)getProjectsOutput;

            _resourceManager = resourceManager;

            IsLoadingProjects = false;
            Projects = new ObservableCollection<Project>();

            LoadProjectsCommand = new DelegateCommand(LoadProjects);
        }

        async void LoadProjects()
        {
            try
            {
                IsLoadingProjects = true;
                await RefreshCollection();
            }
            finally
            {
                IsLoadingProjects = false;
            }
        }

        private async Task RefreshCollection()
        {
            Projects.Clear();
            await _getProjectsUseCase.ExecuteAsync(new GetProjectsInput());
            foreach (var project in _getProjectsOutput.Projects)
            {
                Projects.Add(project);
            }
        }

        private bool _isLoadingProjects;
        public bool IsLoadingProjects
        {
            get => _isLoadingProjects;
            set => SetProperty(ref _isLoadingProjects, value);
        }

        private IList<Project> _projects;
        public IList<Project> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }
    }
}
