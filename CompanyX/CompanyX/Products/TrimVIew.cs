namespace CompanyX.Products
{
    using AsNum.XFControls;
    using System.Collections.Generic;
    using Xamarin.Forms;


    public class TrimView : StackLayout
    {

        public TrimView CreateView(TrimView result)
        {
            return GetLayout(result);
        }

        public string ProductName { get { return "Trim"; } }

        public int ProductId { get { return 1; } }
        public double PercentageUsed { get; set; }
        public string MaintenanceDoneAtTime { get; set; }
        public string PowerSupplyAttached { get; set; }

        public string Comments { get; set; }

        public bool DoesTrimNeedImmediateReplacement { get; set; }

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
            DoesTrimNeedImmediateReplacement = ((CheckBox)sender).Checked;
        }



        public TrimView GetLayout(TrimView result)
        {
            result.BindingContext = result;

            result.Children.Add(new Label { Text = "Trim", FontSize = 18, TextColor = Color.Black });
            result.Children.Add(new Label { Text = "How much Trim Used", FontSize = 16, ClassId = "TrimUsedLabel", TextColor = Color.Black });

            var slider = new Slider { ClassId = "PercentageUsed" };
            slider.SetBinding(Slider.ValueProperty, new Binding { Source = result.PercentageUsed });
            result.Children.Add(slider);

            result.Children.Add(new Label { Text = "Power Supply attached with Trim?", FontSize = 16, TextColor = Color.Black });

            var powerAttachedRadio = new RadioGroup { ItemsSource = new List<string> { "Yes", "No" } };
            powerAttachedRadio.PropertyChanged += PowerAttachedRadio_PropertyChanged;
            result.Children.Add(powerAttachedRadio);

            result.Children.Add(new Label { Text = "Maintenance Done On Time?", FontSize = 16, TextColor = Color.Black });

            var maintenanceRadio = new RadioGroup { ItemsSource = new List<string> { "Yes", "No" } };
            maintenanceRadio.PropertyChanged += MaintenanceRadio_PropertyChanged;
            result.Children.Add(maintenanceRadio);


            var doesTrimNeedImmediateReplacement = new CheckBox { ShowLabel = true, Text = "Does Trim Need Immediate Replacement?" };
            doesTrimNeedImmediateReplacement.PropertyChanged += CheckBox_PropertyChanged;
            result.Children.Add(doesTrimNeedImmediateReplacement);



            var comments = new Entry { Placeholder = "Commets", ClassId = "TrimComments", FontSize = 16, TextColor = Color.Black };
            //comments.SetBinding(Entry.TextProperty, new Binding { Source = result.Comments });
            result.Children.Add(comments);


            result.Margin = 5;
            result.Padding = 5;
            return result;
        }


    }
}
