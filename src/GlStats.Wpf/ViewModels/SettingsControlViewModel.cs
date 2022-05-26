using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Resources;
using GlStats.Core.Boundaries.Infrastructure;
using LiteDB;
using Microsoft.Win32;

namespace GlStats.Wpf.ViewModels;

public class SettingsControlViewModel : BindableBase
{
    private readonly IAuthentication _auth;
    private readonly ResourceManager _resourceManager;
    private readonly LiteDatabase _database;

    public DelegateCommand SaveCommand { get; private set; }
    public DelegateCommand ExportDatabaseCommand { get; private set; }

    public SettingsControlViewModel(IAuthentication auth, ResourceManager resourceManager, LiteDatabase database)
    {
        _auth = auth;
        _resourceManager = resourceManager;
        _database = database;

        SelectedLanguage = CultureToLanguage(_auth.GetConfig().CurrentCulture);

        Languages = new ObservableCollection<string>
        {
            _resourceManager.GetString("English"),
            _resourceManager.GetString("German"),
        };

        SaveCommand = new DelegateCommand(SaveSettings);
        ExportDatabaseCommand = new DelegateCommand(ExportDatabase);
    }

    private void SaveSettings()
    {
        string cultureName = "en-UK";
        if (SelectedLanguage == _resourceManager.GetString("English"))
            cultureName = "en-UK";
        else if (SelectedLanguage == _resourceManager.GetString("German"))
            cultureName = "de-DE";
        _auth.UpdateSettings(cultureName);
    }

    private string CultureToLanguage(string language)
    {
        switch (language)
        {
            case "en-UK":
                return _resourceManager.GetString("English");

            case "de-DE":
                return _resourceManager.GetString("German");

            default:
                return _resourceManager.GetString("English");
        }
    }

    private void ExportDatabase()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.FileName = "gl-stats.db";
        saveFileDialog.Filter = "Database (*.db)|*.db";
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        if (saveFileDialog.ShowDialog() == true)
        {
            var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(dataFolder, "gl-stats");

            File.Copy($@"{appFolder}\gl-stats.db", saveFileDialog.FileName);
        }
    }

    private string _selectedLanguage;
    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set => SetProperty(ref _selectedLanguage, value);
    }

    private ObservableCollection<string> _languages;
    public ObservableCollection<string> Languages
    {
        get => _languages;
        set => SetProperty(ref _languages, value);
    }
}

