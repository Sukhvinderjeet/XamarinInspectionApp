namespace CompanyX.ViewModel.Inspection
{
    using CompanyX.Common;
    using CompanyX.Common.Model;
    using CompanyX.Manager;
    using CompanyX.Products;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Plugin.SecureStorage;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class InspectionViewModel : BaseViewModel, INotifyPropertyChanged
    {
        InspectionManager inspectionManager;

        private object obj = new object();
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string _selectedProduct;
        private ObservableCollection<InspectionAttachmentsModel> _images;
        private InspectionAttachmentsModel _imageModel;
        private string _location;
        private List<string> _productList;
        private string _dueDate;
        private InspectionModel inspectionModel;
        public List<string> ProductList
        {
            get { return _productList; }
            set
            {
                _productList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProductList"));
            }
        }
        public InspectionViewModel(InspectionModel inspectionModel)
        {
            Images = new ObservableCollection<InspectionAttachmentsModel>();

            this.inspectionModel = inspectionModel;
            ProductList = new List<string> { "Roller", "Trim" };
            inspectionManager = new InspectionManager();
        }

        public string Location
        {
            get => _location; set
            {
                _location = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            }
        }

        public string DueDate
        {
            get => _dueDate; set
            {
                _dueDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DueDate"));
            }
        }

        public bool ShowAddProductPicker
        {
            get { return ProductList.Any(); }
        }

        public string InspectionDate { get { return DateTime.Now.ToString("MMMM dd, yyyy"); } }
        public string UserName { get { return CrossSecureStorage.Current.GetValue(Constants.User.LoggedInUserId); } }
        public bool ShowAttachmentsList { get { return Images.Any(); } }

        public ObservableCollection<InspectionAttachmentsModel> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Images"));
            }
        }

        public InspectionAttachmentsModel ImageModel
        {
            get { return _imageModel; }
            set
            {
                _imageModel = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageModel"));
            }
        }

        public string SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedProduct"));
                OnAddNewProduct();
                PropertyChanged(this, new PropertyChangedEventArgs("ShowAddProductPicker"));
            }
        }
        public Action<StackLayout> AddNewProductToView = (view) => { };

        public Action RedirectToInspectionList = () => { };


        public ICommand AddNewAttachmentButtonCommand
        {
            get
            {
                return new Command(OnAddNewAttachmentButtonCommand);
            }
        }
        public RollerView Roller { get; set; }
        public TrimView Trim { get; set; }
        private void OnAddNewProduct()
        {
            if (!string.IsNullOrEmpty(SelectedProduct))
            {
                switch (SelectedProduct.ToLower())
                {
                    case "roller":
                        Roller = new RollerView();
                        Roller = Roller.CreateView(Roller);
                        AddNewProductToView(Roller);
                        break;
                    case "trim":
                        Trim = new TrimView();
                        Trim = Trim.CreateView(Trim);
                        AddNewProductToView(Trim);
                        break;
                    default:
                        break;
                }
                ProductList = ProductList.Where(x => x != SelectedProduct).ToList();
                SelectedProduct = null;
            }
        }


        public ICommand SubmitButtonCommand
        {
            get
            {
                return new Command(OnSubmitButtonCommand);
            }
        }

        private void OnSubmitButtonCommand()
        {
            IsWorking = true;
            ProcessSubmitRequest();
        }

        private async Task ProcessSubmitRequest()
        {
            await Task.Delay(50);
            inspectionModel.Details = new List<string>();
            inspectionModel.UserID = UserName;
            inspectionModel.IsCompleted = true;


            if (Roller != null)
            {
                inspectionModel.Details.Add(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    ProductName = Roller.ProductName,
                    ProductId = Roller.ProductId,
                    PercentageUsed = Roller.PercentageUsed,
                    MaintenanceDoneAtTime = Roller.MaintenanceDoneAtTime,
                    PowerSupplyAttached = Roller.PowerSupplyAttached,
                    Comments = Roller.Comments,
                    DoesRollerNeedImmediateReplacement = Roller.DoesRollerNeedImmediateReplacement
                }));
            }
            if (Trim != null)
            {
                inspectionModel.Details.Add(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    ProductName = Trim.ProductName,
                    ProductId = Trim.ProductId,
                    PercentageUsed = Trim.PercentageUsed,
                    MaintenanceDoneAtTime = Trim.MaintenanceDoneAtTime,
                    PowerSupplyAttached = Trim.PowerSupplyAttached,
                    Comments = Trim.Comments,
                    DoesRollerNeedImmediateReplacement = Trim.DoesTrimNeedImmediateReplacement
                }));
            }
            if (Images != null)
            {
                inspectionModel.Attachments = new List<byte[]>();
                foreach (var image in Images)
                {
                    inspectionModel.Attachments.Add(image.Image);
                }
            }
            try
            {
                inspectionManager.Submit(inspectionModel);
                Analytics.TrackEvent(string.Format("User {0} submitted an inspection at :{1}", UserName, DateTime.Now.ToShortDateString()));
                RedirectToInspectionList();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
            finally
            {
                IsWorking = false;
            }
        }

        private bool _isWorking;

        public bool IsWorking
        {
            get { return _isWorking; }
            set
            {
                _isWorking = value;
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.Page.IsWorking));
            }
        }
        public static byte[] ConvertStreamTOByteArray(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
        private async void OnAddNewAttachmentButtonCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }


            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = false,
                CompressionQuality = 80,
                CustomPhotoSize = 60,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 1000,
                DefaultCamera = CameraDevice.Rear
            });


            if (file != null)
            {
                var inspectionAttachmentsModel = new InspectionAttachmentsModel { };

                var img = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                inspectionAttachmentsModel.Image = ConvertStreamTOByteArray(file.GetStream());
                inspectionAttachmentsModel.ImageSource = img;
                Images.Add(inspectionAttachmentsModel);
                PropertyChanged(this, new PropertyChangedEventArgs("Images"));
                PropertyChanged(this, new PropertyChangedEventArgs("ShowAttachmentsList"));
            }

        }

        
    }


}
