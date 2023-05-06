using Data.Entities;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.GPT3;
using System.Runtime.Serialization;

namespace Data.Services;
public class AiService
{
	private readonly UserSettings _userSettings;

	public AiService(UserSettings userSettings)
	{
		_userSettings = userSettings;
	}

	public async Task<string> GenerateResponseAsync(string prompt, int? maxTokens = null, float temperature = 0.7f)
	{

		if(_userSettings.ApiKey == null)
		{
			throw new ApiKeyNotSetException();
		}

		var openAiService = new OpenAIService(new OpenAiOptions()
		{
			ApiKey = _userSettings.ApiKey
		});

		var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
		{
			Messages = new List<ChatMessage>
			{
				ChatMessage.FromUser(prompt)
			},
			Model = _userSettings.SelectedModel ?? Models.ChatGpt3_5Turbo,
			MaxTokens = maxTokens,
			Temperature = temperature
		});

		if (!completionResult.Successful)
		{
			var errorMessage = completionResult.Error?.Message;
			throw new AiServiceException(errorMessage);
		}

		return completionResult.Choices.First().Message.Content;
		
	}
}

internal class AiServiceException : Exception
{
	public AiServiceException(string? message) : base(message)
	{
	}
}

internal class ApiKeyNotSetException : Exception
{
	public ApiKeyNotSetException()
	{
	}
}