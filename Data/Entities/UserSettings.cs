namespace Data.Entities;
public class UserSettings
{
	/// <summary>
	/// The path to directory with project files
	/// </summary>
	public string Workspace { get; set; } = Path.Combine(new[] { "data", "projects" });
	public string? ApiKey { get; set; }
	public string? SelectedModel { get; set; }
}
