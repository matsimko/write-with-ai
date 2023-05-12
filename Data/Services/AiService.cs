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
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;

namespace Data.Services;
public class AiService
{
    private readonly IDataService _dataService;
	private UserSettings Settings { get => _dataService.UserSettings; }

    public AiService(IDataService dataService)
	{
        _dataService = dataService;
    }

	public async Task<string> GenerateResponseAsync(string prompt, int? maxTokens = null, float temperature = 0.7f)
	{
		var openAiService = CreateOpenAiService();
		var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
		{
			Messages = new List<ChatMessage>
			{
				ChatMessage.FromUser(prompt)
			},
			Model = Settings.SelectedModel ?? Models.ChatGpt3_5Turbo,
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

	public async Task<List<string>> GetAvailableModelsAsync()
	{
        var openAiService = CreateOpenAiService();
		var response = await openAiService.ListModel();

		var modelRegex = @"^gpt-(\d+)(\.\d+)?";

		var availableModels = new List<string>();
        foreach (var model in response.Models)
		{
			if(Regex.IsMatch(model.Id, modelRegex))
			{
				availableModels.Add(model.Id);
			}
		}
		return availableModels;
    }

	private OpenAIService CreateOpenAiService()
	{
        if (Settings.ApiKey == null)
        {
            throw new ApiKeyNotSetException();
        }

        var openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = Settings.ApiKey
        });

		return openAiService;
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