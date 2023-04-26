using Microsoft.Extensions.Logging;
using MyFridge_UserInterface_MAUI.Service.UoW;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;
using MyFridge_UserInterface_MAUI.View;
using MyFridge_UserInterface_MAUI.ViewModel;
using MyFridge_UserInterface_MAUI.ViewModel.Detail;

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

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddTransient<GroceryViewModel>();
            builder.Services.AddTransient<DetailGroceryViewModel>();
            builder.Services.AddTransient<DetailIngredientViewModel>();
            builder.Services.AddTransient<RecipeViewModel>();
            builder.Services.AddTransient<DetailRecipeViewModel>();
            builder.Services.AddTransient<UserLoginViewModel>();
            builder.Services.AddTransient<UserIngredientViewModel>();
            builder.Services.AddTransient<UserViewModel>();

            builder.Services.AddTransient<UserLoginPage>();
            builder.Services.AddTransient<UserInfoPage>();
            builder.Services.AddTransient<UserIngredientPage>();
            builder.Services.AddTransient<GroceryPage>();
            builder.Services.AddTransient<RecipePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}