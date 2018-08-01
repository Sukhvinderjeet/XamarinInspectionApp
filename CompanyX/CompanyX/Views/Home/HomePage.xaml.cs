
using CompanyX.Common;
using CompanyX.Manager;
using CompanyX.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanyX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {

            var vm = new HomeViewModel();
            BindingContext = vm;
            vm.RedirectToLoginPage += () =>
            {
                Navigation.PopToRootAsync().Wait();
                Navigation.PushModalAsync(new LoginPage());
            };
            vm.RedirectToInspectionList += () =>
            {
                Navigation.PushModalAsync(new NavigationPage(new InspectionList()));
            };
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IAndroidMethods>().CloseApp();

            return base.OnBackButtonPressed();
            
        }
    }
}