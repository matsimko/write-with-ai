using System.Text.Json.Serialization;

namespace Data.Entities;

[JsonDerivedType(typeof(FictionProject), "FictionProject")]
[JsonDerivedType(typeof(NonfictionProject), "NonfictionProject")]
public abstract class WritingProject
{
	public Guid Id { get; set; } = Guid.NewGuid();

	/// <summary>
	/// Name of the project
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Rules for writing are a part of every prompt
	/// </summary>
	public List<string> Rules { get; } = new();

	/// <summary>
	/// Main genre is a part of every prompt
	/// </summary>
	public string? MainGenre { get; set; }

	/// <summary>
	/// Description is a part of the initial prompt used for generating the outline
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Title of the writing
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	/// Outline is a part of every prompt
	/// </summary>
    public string? Outline { get; set; }

    public List<WritingPart>? Parts { get; set; }
}