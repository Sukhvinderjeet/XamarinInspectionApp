using System.Collections.Generic;
using CompanyX.Common.Model;

namespace CompanyX.Manager
{
    public interface IInspectionManager
    {
        void FetchDataFromServer();

        List<InspectionModel> GetAllInspection();

        int GetInspectionCount();
    }
}