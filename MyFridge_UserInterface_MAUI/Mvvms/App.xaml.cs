namespace MyFridge_UserInterface_MAUI.Mvvms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}