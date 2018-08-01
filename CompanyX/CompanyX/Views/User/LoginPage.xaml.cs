namespace CompanyX
{
    using CompanyX.Common;
    using CompanyX.Manager;
    using CompanyX.ViewModel;
    using CompanyX.Views;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm;
        public LoginPage()
        { 
            vm = new LoginViewModel(new UserManager());
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += (error) =>
            {
                DisplayAlert("Error", error, "Ok");
            };  
            vm.RedirectToHome += () =>
            { 
                Navigation.PushModalAsync(new NavigationPage(new Views.HomePage()));
            };

            vm.RedirectToSignUp += () =>
            {
                Navigation.PushModalAsync(new SignUpPage());
            };

            InitializeComponent();
            this.AddProgressDisplay();
        }
        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IAndroidMethods>().CloseApp();

            return base.OnBackButtonPressed();

        }
    }
}