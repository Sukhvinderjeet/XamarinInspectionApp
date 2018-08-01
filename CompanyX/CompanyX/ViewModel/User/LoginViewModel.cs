namespace CompanyX.ViewModel
{
    using CompanyX.Common;
    using CompanyX.Common.Model;
    using CompanyX.Manager;
    using CompanyX.ValidationRule;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : UserViewModel, INotifyPropertyChanged
    {

        #region Private
        private IUserManager userManager;


        #endregion

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public bool IsLoginFormValid { get { return emailValidationRules.All(x => x.IsRuleValid(Email)) && passwordValidationRules.All(p => p.IsRuleValid(Password)); } }
        public Action<string> DisplayInvalidLoginPrompt = (errorMessage) => { };
        public Action RedirectToHome = () => { };
        public Action RedirectToSignUp = () => { };
        #region Public Properties

        public LoginViewModel(IUserManager userManager)
        {
            this.userManager = userManager;
            base.PropertyChangedAction += (propertyName) =>
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            };
        }

        public ICommand LoginButtonCommand
        {
            get
            {
                return new Command(OnLoginButtonCommand);
            }
        }

        public ICommand SignupButtonCommand
        {
            get
            {
                return new Command(OnSignupButtonCommand);
            }
        }

        public void OnSignupButtonCommand()
        {
            RedirectToSignUp();
        }

        public void OnLoginButtonCommand()
        {
            if (IsLoginFormValid)
            {
                IsWorking = true;
                Login();
            }
            else
            {
                IsWorking = false;
                ValidateFormChanges(Constants.Page.Email);
                ValidateFormChanges(Constants.Page.Password);
            }

        }
        private async Task<bool> Login()
        {
            await Task.Delay(100);
            var result = false;
            LoginRequestModel loginRequestModel = new LoginRequestModel { UserName = Email, Password = Password };
            try
            {
                userManager.LogIn(loginRequestModel);
                RedirectToHome();
                result = true;
                Analytics.TrackEvent(string.Format("User {0} Logined at:{1}", Email, DateTime.Now.ToShortDateString()));
            }
            catch (Exception exception)
            {
                DisplayInvalidLoginPrompt("Invalid username or password. Please try again");
                Crashes.TrackError(exception);
            }
            finally
            {
                IsWorking = false;
            }
            return result;
        }
    }
    #endregion
}

