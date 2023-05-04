using System.Text.Json.Serialization;

namespace Data.Entities;

[JsonDerivedType(typeof(FictionProject), "FictionProject")]
[JsonDerivedType(typeof(NonfictionProject), "NonfictionProject")]
public class WritingProject
{
	public Guid Id { get; set; } = Guid.NewGuid();

	/// <summary>
	/// Rules for writing are a part of every prompt
	/// </summary>
	public required List<string> Rules { get; set; }

	/// <summary>
	/// Description is a part of the initial prompt used for generating the outline
	/// </summary>
	public string? Description { get; set; }

	public string? Title { get; set; }

	/// <summary>
	/// Outline is a part of every prompt
	/// </summary>
    public string? Outline { get; set; }

    public List<WritingPart>? Parts { get; set; }
}
