using Microsoft.Extensions.Logging;
using MyFridge_UserInterface_MAUI.Service;
using MyFridge_UserInterface_MAUI.ViewModel;
using MyFridge_UserInterface_MAUI.Views;

namespace MyFridge_UserInterface_MAUI
{
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
                });

            builder.Services.AddSingleton<UserService>();

            builder.Services.AddTransient<UserViewModel>();
            builder.Services.AddTransient<IngredientViewModel>();
            builder.Services.AddTransient<RecipyViewModel>();

            builder.Services.AddTransient<UserLoginPage>();
            builder.Services.AddTransient<UserInfoPage>();
            builder.Services.AddTransient<UserIngredientPage>();
            builder.Services.AddTransient<IngredientPage>();
            builder.Services.AddTransient<RecipyPage>();

#if DEBUG
		    builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}