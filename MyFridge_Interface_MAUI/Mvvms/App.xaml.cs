namespace MyFridge_Interface_MAUI.Mvvms
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