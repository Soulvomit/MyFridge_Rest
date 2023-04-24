using MyFridge_UserInterface_MAUI.Views;

namespace MyFridge_UserInterface_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
            Routing.RegisterRoute(nameof(UserLogoutPage), typeof(UserLogoutPage));
            Routing.RegisterRoute(nameof(UserInfoPage), typeof(UserInfoPage));
            Routing.RegisterRoute(nameof(UserIngredientPage), typeof(UserIngredientPage));
            Routing.RegisterRoute(nameof(IngredientPage), typeof(IngredientPage));
            Routing.RegisterRoute(nameof(RecipePage), typeof(RecipePage));
        }
    }
}