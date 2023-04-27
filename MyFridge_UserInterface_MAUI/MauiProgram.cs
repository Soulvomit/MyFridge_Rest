using Microsoft.Extensions.Logging;
using MyFridge_UserInterface_MAUI.Mvvms;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Client;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Client.Interface;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Navigation.Interface;

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

            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.RegisterViewModels();
            builder.Services.RegisterPages();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}