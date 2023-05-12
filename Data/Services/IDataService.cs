using Data.Entities;

namespace Data.Services;
public interface IDataService
{
    UserSettings UserSettings { get; }

    event EventHandler? UserSettingsChanged;

    Task InitAsync();
    void DeleteProject(WritingProject project);
    List<WritingProject> LoadProjects();
    void SaveProject(WritingProject project);
    Task SaveUserSettingsAsync(UserSettings userSettings);
}