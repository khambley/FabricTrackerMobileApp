using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FabricTrackerMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(Resolver.Resolve<MainPage>())
            {
                BarBackgroundColor = Color.FromHex("#3FA796"),
                BarTextColor = Color.White,
                Title = "My Fabric Tracker"
            };
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
