namespace CompanyX
{
    using CompanyX.Common;
    using CompanyX.Common.Model;
    using CompanyX.Manager;
    using CompanyX.ValidationRule;
    using Microsoft.AppCenter.Crashes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class SignUpViewModel : UserViewModel, INotifyPropertyChanged
    {
        private IUserManager userManager;

        public Action RedirectToLoginPage = () => { };

        public SignUpViewModel(IUserManager userManager)
        {
            this.userManager = userManager;
            base.PropertyChangedAction += (propertyName) =>
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            };
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public Action<string> DisplayInvalidLoginPrompt = (errorMessage) => { };
        public bool IsLoginFormValid
        {
            get
            {
                return emailValidationRules.All(x => x.IsRuleValid(Email)) &&
                     firstValidationRules.All(x => x.IsRuleValid(FirstName))
                    && passwordValidationRules.All(p => p.IsRuleValid(Password));
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
            IsWorking = true;
            if (IsLoginFormValid)
            {
                SignUp();
            }
            else
            {
                IsWorking = false;
                ValidateFormChanges(Constants.Page.Email);
                ValidateFormChanges(Constants.Page.Password);
                ValidateFormChanges(Constants.Page.FirstName);
            }
        }

        public async void SignUp()
        {
            try
            {
                await Task.Delay(100);
                SignUpRequestModel signUpRequestModel = new SignUpRequestModel { Email = Email, Password = Password, FirstName = FirstName, LastName = LastName };
                userManager.SignUp(signUpRequestModel);
                RedirectToLoginPage();
            }
            catch (Exception exception)
            {
                DisplayInvalidLoginPrompt(exception.Message);
                Crashes.TrackError(exception);
            }
            finally
            {
                IsWorking = false;
            }
        }
    }
}
