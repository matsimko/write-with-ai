using Data.Services;
using Microsoft.Extensions.Logging;

namespace UI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

		builder.Services.AddSingleton<IDataService, FileDataService>();		
		builder.Services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IDataService>().UserSettings);
		builder.Services.AddSingleton<AiService>();
		builder.Services.AddSingleton<WritingService>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		Directory.SetCurrentDirectory(FileSystem.Current.AppDataDirectory);

		return builder.Build();
	}
}
