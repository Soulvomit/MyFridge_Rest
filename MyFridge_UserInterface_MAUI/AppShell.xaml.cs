using MyFridge_UserInterface_MAUI.Views;

namespace MyFridge_UserInterface_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
            Routing.RegisterRoute(nameof(UserInfoPage), typeof(UserInfoPage));
            Routing.RegisterRoute(nameof(UserIngredientPage), typeof(UserIngredientPage));
            Routing.RegisterRoute(nameof(RecipyPage), typeof(RecipyPage));
        }
    }
}