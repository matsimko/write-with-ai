using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Data.Entities;
public class UserSettings
{
    //exposing setters to allow binding in Blazor.

    /// <summary>
    /// The path to directory with project files
    /// </summary>
    public string Workspace { get; set; } = Path.Combine(new[] { Directory.GetCurrentDirectory(), "data", "projects" });

    [JsonIgnore]
    public string? ApiKey { get; set; }

    public string? SelectedModel { get; set; }

    public UserSettings()
    {
    }

    public UserSettings(UserSettings settings)
    {
        settings.ShallowCopyPropertiesTo(this);
    }

    public void Update(UserSettings settings)
    {
        settings.ShallowCopyPropertiesTo(this);
    }

}
