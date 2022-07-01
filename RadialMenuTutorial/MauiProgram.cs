using CommunityToolkit.Maui;
using Microsoft.AspNetCore.Components.WebView.Maui;
using RadialMenuTutorial.Data;
using RadialMenuTutorial.Pages;
using RadialMenuTutorial.ViewModels;

namespace RadialMenuTutorial;

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
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		builder.UseMauiCommunityToolkit();

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddSingleton<EditMenuViewModel>();
        builder.Services.AddTransient<EditMenuPage>();

        return builder.Build();
	}
}
