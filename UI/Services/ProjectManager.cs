using Data.Entities;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services;
internal class ProjectManager
{
    private readonly IDataService _dataService;
    private string _workspace;
    public ObservableCollection<WritingProject> Projects { get; private set; }
    public WritingProject? SelectedProject { get; set; }

    public ProjectManager(IDataService dataService)
    {
        _dataService = dataService;
        _workspace = _dataService.UserSettings.Workspace;
        _dataService.UserSettingsChanged += OnUserSettingsChanged;
        LoadProjects();
    }

    private void OnUserSettingsChanged(object? sender, EventArgs e)
    {
        var newWorkspace = _dataService.UserSettings.Workspace;
        if (_workspace != newWorkspace)
        {
            _workspace = newWorkspace;
            LoadProjects();
        }
    }

    [MemberNotNull(nameof(Projects))]
    private void LoadProjects()
    {
        Projects = new(_dataService.LoadProjects());
        Projects.CollectionChanged += Projects_OnChanged;

    }

    private void Projects_OnChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if(e.Action == NotifyCollectionChangedAction.Move) 
        {
            return;
        }

        if(e.OldItems != null)
        {
            foreach (WritingProject p in e.OldItems)
            {
                _dataService.DeleteProject(p);
                if(p == SelectedProject)
                {
                    SelectedProject = null;
                }
            }
        }

        if (e.NewItems != null)
        {
            foreach (WritingProject p in e.NewItems)
            {
                _dataService.SaveProject(p);
            }
        }
    }
}
