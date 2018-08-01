namespace CompanyX.Manager
{
    using CompanyX.Common;
    using CompanyX.Common.Integration.Provider;
    using CompanyX.Common.Model;
    using CompanyX.Data;
    using CompanyX.Data.Model;
    using CompanyX.Data.Repository;
    using Plugin.Connectivity;
    using Plugin.SecureStorage;
    using System.Collections.Generic;
    using System.Linq;

    public class InspectionManager : IInspectionManager
    {

        InspectionRepository inspectionRepository;
        public InspectionManager()
        {
            DatabaseInit.Initialize();
            inspectionRepository = new InspectionRepository(DatabaseInit.Connection);
        }
        public List<InspectionModel> GetAllInspection()
        {
            return inspectionRepository.GetItems(CrossSecureStorage.Current.GetValue(Constants.User.LoggedInUserId)).Select(x => new InspectionModel
            {
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                DueDate = x.DueDate,
                Id = x.Id,
                ServerId = x.ServerId,
                InspectionDetails = x.InspectionDetails,
                Location = x.Location
            }).ToList();
        }


        public int GetInspectionCount()
        {
            return inspectionRepository.GetItemsCount(CrossSecureStorage.Current.GetValue(Constants.User.LoggedInUserId));
        }

        public void FetchDataFromServer()
        {
            if (ValidateNetworkConnectivity())
            {
                var headers = new Dictionary<string, string>();
                headers.Add(Constants.Headers.AuthorizationKey, string.Format(Constants.Headers.AuthorizationValueFormat, CrossSecureStorage.Current.GetValue(Constants.User.AuthorizationToken)));
                headers.Add(Constants.Headers.ContentType, "application/json");

                var url = string.Format("{0}/{1}", Constants.ServerUri.BaseUrl, "api/Inspection");
                var p = new CompanyXHttpClient();
                var result = p.Get<List<InspectionModel>>("api/Inspection", headers);

                var inspectionEntities = result.Select(x => new Data.Model.InspectionEntity
                {
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    ServerId = x.Id,
                    InspectionDetails = x.InspectionDetails,
                    Location = x.Location,
                    UserID = x.UserID,
                    IsCompleted = false,
                    IsSubmitted = false
                }).ToList();
                inspectionRepository.SaveItems(inspectionEntities);
            }
        }

        /// <summary>
        ///  Network connectivity
        /// </summary>
        private bool ValidateNetworkConnectivity()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public void Submit(InspectionModel inspectionModel)
        {
            var inspection = new InspectionEntity()
            {
                Id = inspectionModel.Id,
                CreatedDate = inspectionModel.CreatedDate,
                Description = inspectionModel.Description,
                DueDate = inspectionModel.DueDate,
                InspectionDetails = inspectionModel.InspectionDetails,
                IsCompleted = true,
                IsSubmitted = true,
                Location = inspectionModel.Location,
                ServerId = inspectionModel.ServerId,
                UserID = inspectionModel.UserID
            };
            if (!SubmitInspectionModelToServer(inspectionModel))
            {
                inspection.IsSubmitted = false;
                inspection.InspectionDetails = Newtonsoft.Json.JsonConvert.SerializeObject(inspectionModel.Details);
                inspection.Attachments = Newtonsoft.Json.JsonConvert.SerializeObject(inspectionModel.Attachments);
            }
            inspectionRepository.SaveItem(inspection);
        }

        private bool SubmitInspectionModelToServer(InspectionModel inspectionModel)
        {
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.Headers.AuthorizationKey, string.Format(Constants.Headers.AuthorizationValueFormat, CrossSecureStorage.Current.GetValue(Constants.User.AuthorizationToken)));
            headers.Add(Constants.Headers.ContentType, "application/json");
             
            var p = new CompanyXHttpClient();
            var result = p.Post<bool>("api/SubmitInspection", inspectionModel, headers);

            return result;
        }

    }
}
