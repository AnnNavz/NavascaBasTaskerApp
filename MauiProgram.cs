using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace NavascaBasTaskerApp
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("Outfit-Regular.ttf", "OutfitRegular");
                    fonts.AddFont("Outfit-Semibold.ttf", "OutfitSemibold");
					fonts.AddFont("Outfit-Black.ttf", "OutfitBlack");
                    fonts.AddFont("Outfit-Bold.ttf", "OutfitBold");
					fonts.AddFont("Outfit-ExtraBold.ttf", "OutfitExtraBold");
                    fonts.AddFont("Outfit-ExtraLight.ttf", "OutfitExtraLight");
					fonts.AddFont("Outfit-Light.ttf", "OutfitLight");
                    fonts.AddFont("Outfit-Medium.ttf", "OutfitMedium");
					fonts.AddFont("Outfit-Thin.ttf", "OutfitThin");
				});

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
