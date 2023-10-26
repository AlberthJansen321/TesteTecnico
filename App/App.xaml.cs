namespace App
{
    public partial class App : Application
    {
        public const string url_base = "http://192.168.1.172:7089/api";
        public static bool updateview = false;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}