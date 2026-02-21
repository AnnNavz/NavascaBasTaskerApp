using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NavascaBasTaskerApp.MVVM.Models;
using NavascaBasTaskerApp.MVVM.ViewModels;
using NavascaBasTaskerApp.MVVM.Views;
using NavascaBasTaskerApp.Views;



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

			builder.Services.AddSingleton<CategoryService>();
			builder.Services.AddTransient<MainViewModel>();
			builder.Services.AddTransient<AddTaskViewModel>();
			builder.Services.AddTransient<AddTask>();
			builder.Services.AddTransient<MainView>();
#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
