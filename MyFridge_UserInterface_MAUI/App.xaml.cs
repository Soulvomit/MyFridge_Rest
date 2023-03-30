using MyFridge_UserInterface_MAUI.Views;

namespace MyFridge_UserInterface_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //MainPage = new NavigationPage(new UserLoginPage());
        }
    }
}