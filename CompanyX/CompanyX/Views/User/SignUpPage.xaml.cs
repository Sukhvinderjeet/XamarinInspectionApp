namespace CompanyX.Views
{
    using CompanyX.Manager;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            var vm = new SignUpViewModel(new UserManager());
            BindingContext = vm;
            vm.RedirectToLoginPage += () =>
            {
                Navigation.PushModalAsync(new LoginPage());
            };
            vm.DisplayInvalidLoginPrompt += (error) =>
            {
                DisplayAlert("Error", error, "Ok");
            };
            InitializeComponent();
            vm.IsWorking = false;
            this.AddProgressDisplay();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new LoginPage());
            return true;
        }
    }
}