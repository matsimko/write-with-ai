using CommunityToolkit.Maui;
using Data.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using UI.Services;

namespace UI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();

        builder.Services.AddSingleton<IDataService, FileDataService>();		
		builder.Services.AddSingleton<AiService>();
		builder.Services.AddSingleton<WritingService>();
        builder.Services.AddSingleton<ProjectManager>();
		builder.Services.AddSingleton<PlatformUtils>();
		builder.Services.AddSingleton<ISecrets, Secrets>();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
		Directory.SetCurrentDirectory(FileSystem.Current.AppDataDirectory);

		var app = builder.Build();

		app.Services.GetRequiredService<IDataService>().InitAsync().Wait();

		return app;
	}
}
