using Data.Entities;

namespace Data.Services;
public class WritingService
{
	private readonly AiService _aiService;

	public WritingService(AiService aiService)
    {
		_aiService = aiService;
	}
}
