using CompanyX.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.SecureStorage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CompanyX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (!Convert.ToBoolean(CrossSecureStorage.Current.GetValue(Common.Constants.User.IsUserLoggedId, "false")))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }
        }



        protected override void OnStart()
        {
            AppCenter.Start("", typeof(Analytics), typeof(Crashes));
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
