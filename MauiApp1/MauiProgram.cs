using Microsoft.Extensions.Logging;

namespace MauiApp1;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("HEADINGNOWTRIAL-04REGULAR.TTF", "Heading");
				fonts.AddFont("BAHNSCHRIFT.TTF", "Lemon");
				fonts.AddFont("BAHNSCHRIFT.TTF", "Cooper");



			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
