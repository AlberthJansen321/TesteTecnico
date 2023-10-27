using App.Views;

namespace App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Update), (typeof(Update)));
            Routing.RegisterRoute(nameof(Add), typeof(Add));
            Routing.RegisterRoute(nameof(Home), typeof(Home));
        }
    }
}