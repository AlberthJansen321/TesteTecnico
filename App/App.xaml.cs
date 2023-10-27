namespace App
{
    public partial class App : Application
    {
        public const string url_base = "http://179.108.74.86:7089/api";
        public static bool updateview = false;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}