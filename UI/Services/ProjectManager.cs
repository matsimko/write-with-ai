using Data.Entities;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services;
internal class ProjectManager
{
    private readonly IDataService _dataService;
    public ObservableCollection<WritingProject> Projects { get; private set; }
    public WritingProject? SelectedProject { get; set; }

    public ProjectManager(IDataService dataService)
    {
        _dataService = dataService;
        LoadProjects();
        _dataService.UserSettings.OnWorkspaceChanged += UserSettings_OnWorkspaceChanged;
    }

    public void SetWorkspace(string workspace)
    {
        _dataService.UserSettings.Workspace = workspace;
        _dataService.SaveUserSettings();
    }

    private void UserSettings_OnWorkspaceChanged(object? sender, EventArgs e)
    {
        LoadProjects();
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
