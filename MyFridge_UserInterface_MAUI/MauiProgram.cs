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

            builder.Services.AddSingleton<CurrentUserService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<IngredientService>();
            builder.Services.AddSingleton<IngredientAmountService>();
            builder.Services.AddSingleton<RecipyService>();
            builder.Services.AddSingleton<UserViewModel>();

            builder.Services.AddTransient<UserLoginViewModel>();
            builder.Services.AddTransient<UserIngredientViewModel>();
            builder.Services.AddTransient<IngredientViewModel>();
            builder.Services.AddTransient<UserIngredientDetailViewModel>();
            builder.Services.AddTransient<RecipyViewModel>();
            builder.Services.AddTransient<RecipyDetailViewModel>();

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