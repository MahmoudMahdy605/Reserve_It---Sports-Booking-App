using SportsBookingApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsBookingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            /*
            string uname = Preferences.Get("UserName", String.Empty);
            if (String.IsNullOrEmpty(uname))
                MainPage = new NavigationPage( new SignInView() );
            
            else MainPage = new NavigationPage(new MainTabbedView());
            */
            

            MainPage = new SignInView();

            /*
            string uname = Preferences.Get("CenterName", String.Empty);
            if (String.IsNullOrEmpty(uname))
                MainPage = new NavigationPage(new SignInView());

            else MainPage = new NavigationPage(new CenterTabbedView());
            */
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
