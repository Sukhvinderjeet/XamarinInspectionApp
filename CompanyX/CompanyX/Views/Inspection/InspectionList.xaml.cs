namespace CompanyX.Views
{
    using CompanyX.Common.Model;
    using CompanyX.Manager;
    using CompanyX.ViewModel;
    using CompanyX.Views.Inspection;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspectionList : ContentPage
    {
        InspectionListViewModel inspectionListViewModel = new InspectionListViewModel(new InspectionManager());
        public InspectionList()
        {

            this.BindingContext = inspectionListViewModel;
            inspectionListViewModel.RedirectToLoginPage += () =>
            {
                Navigation.PopToRootAsync().Wait();
                Navigation.PushModalAsync(new LoginPage());
            };

            inspectionListViewModel.BackAction += () =>
            {
                Navigation.PopModalAsync();
            };
            inspectionListViewModel.RedirectToHome += () =>
            {
                Navigation.PushModalAsync(new HomePage());
            };
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new InspectionPage((InspectionModel)e.SelectedItem)));
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavigationPage(new HomePage()));
            return true;
        }
    }
}