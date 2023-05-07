namespace Data.Entities;
public class UserSettings
{
	
    private string _workspace = Path.Combine(new[] { "data", "projects" });

    /// <summary>
	/// The path to directory with project files
	/// </summary>
    public string Workspace
    {
        get => _workspace;
        set
        {
            if (_workspace != value)
            {
                _workspace = value;
                OnWorkspaceChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public event EventHandler? OnWorkspaceChanged;

    public string? ApiKey { get; set; }
	public string? SelectedModel { get; set; }
}
