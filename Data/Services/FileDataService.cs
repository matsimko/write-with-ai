using Data.Entities;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Data.Services;
public class FileDataService : IDataService
{
	private static readonly string _settingsFilePath = Path.Combine(new[] { "data", "settings.json" });
	private readonly ILogger<FileDataService> _logger;

	public UserSettings UserSettings { get; private set; }

	public FileDataService(ILogger<FileDataService> logger)
	{
		_logger = logger;

		LoadUserSettings();
	}

	[MemberNotNull(nameof(UserSettings))]
	private void LoadUserSettings()
	{
		try
		{
			UserSettings = LoadFromJsonFile<UserSettings>(_settingsFilePath) ?? new();
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Failed to load UserSettings");
			UserSettings = new();
			SaveUserSettings();
		}
	}

	public void SaveUserSettings()
	{
		SaveAsJsonFile(UserSettings, _settingsFilePath);
	}

	public List<WritingProject> LoadProjects()
	{
		var projects = new List<WritingProject>();
		foreach (var path in Directory.EnumerateFiles(UserSettings.Workspace, "*.json"))
		{
			try
			{
				var project = LoadFromJsonFile<WritingProject>(path);
				if (project != null)
				{
					projects.Add(project);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to load project {path}", path);
			}
		}

		projects.Sort((p1, p2) => p1.CreationDate.CompareTo(p2.CreationDate));
		return projects;
	}

	public void SaveProject(WritingProject project)
	{
		SaveAsJsonFile(project, GetPathForProject(project));
	}

	public void DeleteProject(WritingProject project)
	{
		var reg = new Regex($"{project.Id}.json$");

		Directory.EnumerateFiles(UserSettings.Workspace)
				 .Where(path => reg.Match(path).Success).ToList()
				 .ForEach(File.Delete);
	}

	private string GetPathForProject(WritingProject project)
	{
		return Path.Combine(UserSettings.Workspace, $"{project.Name}_{project.Id}.json");
	}

	private static T? LoadFromJsonFile<T>(string path)
	{
		var json = File.ReadAllText(path);
		return JsonSerializer.Deserialize<T>(json);
	}

	private static void SaveAsJsonFile<T>(T obj, string path)
	{
		string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = true});
		var fileInfo = new FileInfo(path);
		if(fileInfo.Directory != null)
		{
			fileInfo.Directory.Create();
			File.WriteAllText(path, json);
		}
	}
}
