namespace CompanyX.ViewModel
{
    using CompanyX.Common;
    using CompanyX.Common.Model;
    using CompanyX.Manager;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class InspectionListViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private InspectionModel _inspection;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public List<InspectionModel> Inspections { get; set; }
        public Action RedirectToHome = () => { };

        public InspectionModel Inspection
        {
            get { return _inspection; }
            set
            {
                _inspection = value;
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.Page.Inspection));
            }
        }
        public InspectionListViewModel(IInspectionManager inspectionManager)
        {
            var pending = inspectionManager.GetInspectionCount();
            if (pending < 5)
            {
                inspectionManager.FetchDataFromServer();
            }
            Inspections = inspectionManager.GetAllInspection();
            PropertyChanged(this, new PropertyChangedEventArgs("Inspections"));
            if (!Inspections.Any() || Inspections.Count == 0)
            {
                RedirectToHome();
            }
        }
    }
}
