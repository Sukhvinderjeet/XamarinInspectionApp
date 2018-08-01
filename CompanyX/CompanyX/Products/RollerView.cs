namespace CompanyX.Products
{
    using AsNum.XFControls;
    using System.Collections.Generic;
    using Xamarin.Forms;


    public class RollerView : StackLayout
    {

        public RollerView CreateView(RollerView result)
        {
            return GetLayout(result);
        }

        public string ProductName { get { return "Roller"; } }

        public int ProductId { get { return 1; } }
        public double PercentageUsed { get; set; }
        public string MaintenanceDoneAtTime { get; set; }
        public string PowerSupplyAttached { get; set; }

        public string Comments { get; set; }

        public bool DoesRollerNeedImmediateReplacement { get; set; }

        private void MaintenanceRadio_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var item = ((RadioGroup)sender).SelectedItem;
            if (item != null)
            {

                MaintenanceDoneAtTime = item.ToString();
            }
        }

        private void PowerAttachedRadio_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var item = ((RadioGroup)sender).SelectedItem;
            if (item != null)
            {

                PowerSupplyAttached = item.ToString();
            }
        }
        private void CheckBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DoesRollerNeedImmediateReplacement = ((CheckBox)sender).Checked;
        }



        public RollerView GetLayout(RollerView result)
        {
            result.BindingContext = result;

            result.Children.Add(new Label { Text = "Roller", FontSize = 18, TextColor = Color.Black });
            result.Children.Add(new Label { Text = "How much Roller Used", FontSize = 16, ClassId = "RollerUsedLabel", TextColor = Color.Black });

            var slider = new Slider { ClassId = "PercentageUsed" };
            slider.SetBinding(Slider.ValueProperty, new Binding { Source = result.PercentageUsed });
            result.Children.Add(slider);

            result.Children.Add(new Label { Text = "Power Supply attached with Roller?", FontSize = 16, TextColor = Color.Black });

            var powerAttachedRadio = new RadioGroup { ItemsSource = new List<string> { "Yes", "No" } };
            powerAttachedRadio.PropertyChanged += PowerAttachedRadio_PropertyChanged;
            result.Children.Add(powerAttachedRadio);

            result.Children.Add(new Label { Text = "Maintenance Done On Time?", FontSize = 16, TextColor = Color.Black });

            var maintenanceRadio = new RadioGroup { ItemsSource = new List<string> { "Yes", "No" } };
            maintenanceRadio.PropertyChanged += MaintenanceRadio_PropertyChanged;
            result.Children.Add(maintenanceRadio);


            var doesRollerNeedImmediateReplacement = new CheckBox { ShowLabel = true, Text = "Does Roller Need Immediate Replacement?" };
            doesRollerNeedImmediateReplacement.PropertyChanged += CheckBox_PropertyChanged;
            result.Children.Add(doesRollerNeedImmediateReplacement);



            var comments = new Entry { Placeholder = "Commets", ClassId = "RollerComments", FontSize = 16, TextColor = Color.Black };
            //comments.SetBinding(Entry.TextProperty, new Binding { Source = result.Comments });
            result.Children.Add(comments);


            result.Margin = 5;
            result.Padding = 5;
            return result;
        }


    }
}
