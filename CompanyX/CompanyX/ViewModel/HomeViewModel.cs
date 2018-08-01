namespace CompanyX.ViewModel
{
    using CompanyX.Common;
    using CompanyX.Manager;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        readonly IInspectionManager inspectionManager = new InspectionManager();
        public HomeViewModel()
        {
            CreateModel();
        }

        private bool _showStartInspectionButton;
        public bool ShowStartInspectionButton
        {
            get { return _showStartInspectionButton; }
            set
            {
                _showStartInspectionButton = value;
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.Page.ShowStartInspectionButton));
            }
        }

        private void CreateModel()
        {
            var pending = inspectionManager.GetInspectionCount();
            Pending = pending.ToString();
            if (pending < 5)
            {
                inspectionManager.FetchDataFromServer();
                pending = inspectionManager.GetInspectionCount();
                Pending = pending.ToString();
            }
            ShowStartInspectionButton = pending > 0;
        }

        private string _pending;
        public string Pending
        {
            get { return string.Format("You have {0} inspection pending", Convert.ToInt32(_pending) > 0 ? _pending : "no"); }
            set
            {
                _pending = value;
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.Page.Pending));

            }
        }
        public ICommand StartInspectionButtonCommand
        {
            get
            {
                return new Command(OnStartInspection);
            }
        }
        public Action RedirectToInspectionList = () => { };

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnStartInspection()
        {
            RedirectToInspectionList();
        }
    }
}
