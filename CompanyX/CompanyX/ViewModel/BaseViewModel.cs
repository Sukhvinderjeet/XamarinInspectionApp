namespace CompanyX.ViewModel
{
    using CompanyX.Common;
    using Plugin.SecureStorage;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class BaseViewModel
    {
        public Action RedirectToLoginPage = () => { };
        public ICommand Logout
        {
            get
            {
                return new Command(OnLogout);
            }
        }

        public Action BackAction = () => { };
        public ICommand Back
        {
            get
            {
                return new Command(OnBack);
            }
        }

        private void OnBack()
        {
            BackAction();
        }

        private void OnLogout()
        {
            CrossSecureStorage.Current.DeleteKey(Constants.User.IsUserLoggedId);
            CrossSecureStorage.Current.DeleteKey(Constants.User.AuthorizationToken);
            RedirectToLoginPage();
        }
    }
}
