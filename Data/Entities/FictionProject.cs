namespace Data.Entities;
public class FictionProject : WritingProject
{
	public List<WritingPart>? Characters { get; set; }
	public WritingPart? World { get; set; }
}
