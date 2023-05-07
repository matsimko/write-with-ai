using Data.Entities;

namespace Data.Services;
public interface IDataService
{
	UserSettings UserSettings { get; }

	void DeleteProject(WritingProject project);
	List<WritingProject> LoadProjects();
	void SaveProject(WritingProject project);
	void SaveUserSettings();
}