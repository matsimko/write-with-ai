namespace Data.Entities;
public class WritingPart
{
	/// <summary>
	/// The text generated based on the outline/text of the parent
	/// </summary>
	public required string Text { get; set; }
    public string? Title { get; set; }
    public IList<WritingPart>? Children { get; set; }
}