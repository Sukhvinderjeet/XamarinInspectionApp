using CompanyX.Common.Model;
using CompanyX.Products;
using CompanyX.ViewModel.Inspection;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanyX.Views.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspectionPage : ContentPage
    {


        public InspectionPage(InspectionModel inspectionModel)
        {
            var vm = new InspectionViewModel(inspectionModel);
            vm.Location = inspectionModel.Location;
            vm.DueDate = inspectionModel.DueDate;
          
            vm.RedirectToLoginPage += () =>
            {
                Navigation.PopToRootAsync().Wait();
                Navigation.PushModalAsync(new LoginPage());
            };

            vm.AddNewProductToView += (view) =>
            {
                Products.Children.Add(view);
            };

            vm.RedirectToInspectionList += () =>
            {
                Navigation.PushModalAsync(new InspectionList());
            };
            this.BindingContext = vm;
            InitializeComponent();
            this.AddProgressDisplay();
        }       
    }
}