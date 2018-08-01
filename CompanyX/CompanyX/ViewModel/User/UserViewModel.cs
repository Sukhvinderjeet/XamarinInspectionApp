namespace CompanyX
{
    using CompanyX.Common;
    using CompanyX.ValidationRule;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class UserViewModel
    {
        private string email;
        private string password;
        private bool _showPasswordValidationMessage;
        private bool _isWorking;
        private bool _showEmailValidationMessage;
        private bool _showFirstNameValidationMessage;

        public bool IsWorking
        {
            get { return _isWorking; }
            set
            {
                _isWorking = value;
                PropertyChangedAction(Constants.Page.IsWorking);
            }
        }
        public List<IValidationRule<string>> passwordValidationRules = new List<IValidationRule<string>> { new PasswordValidationRule() };
        public List<IValidationRule<string>> emailValidationRules = new List<IValidationRule<string>> { new IsNotNullOrEmptyValidationRule(), new EmaiValidationRule() };
        public Action<string> PropertyChangedAction = (propertyName) => { };
        private string _firstName;
        private string _lastName;

        public List<IValidationRule<string>> firstValidationRules = new List<IValidationRule<string>> { new IsNotNullOrEmptyValidationRule() };


        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                PropertyChangedAction(Constants.Page.LastName);
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                PropertyChangedAction(Constants.Page.FirstName);
            }
        }
        public string EmailValidationMessage { get; set; }
        public string PasswordValidationMessage { get; set; }
        public string FirstNameValidationMessage { get; set; }
        public bool ShowEmailValidationMessage
        {
            get { return _showEmailValidationMessage; }
            set
            {
                _showEmailValidationMessage = value;
                PropertyChangedAction(Constants.Page.ShowEmailValidationMessage);

            }
        }
        public bool ShowPasswordValidationMessage
        {
            get { return _showPasswordValidationMessage; }
            set
            {
                _showPasswordValidationMessage = value;
                PropertyChangedAction(Constants.Page.ShowPasswordValidationMessage);

            }
        }

        public bool ShowFirstNameValidationMessage
        {
            get { return _showFirstNameValidationMessage; }
            set
            {
                _showFirstNameValidationMessage = value;
                PropertyChangedAction(Constants.Page.ShowFirstNameValidationMessage);

            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChangedAction(Constants.Page.Email);
                ValidateFormChanges(Constants.Page.Email);
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChangedAction(Constants.Page.Password);
                ValidateFormChanges(Constants.Page.Password);
            }
        }
        public void ValidateFormChanges(string propertyName)
        {
            switch (propertyName)
            {

                case Constants.Page.Email:
                    if (emailValidationRules.Any(x => !x.IsRuleValid(email)))
                    {
                        var errorMsg = emailValidationRules
                      .FirstOrDefault(x => !x.IsRuleValid(email)).ValidationMessage(email);

                        EmailValidationMessage = errorMsg;
                        ShowEmailValidationMessage = true;
                    }
                    else
                    {
                        ShowEmailValidationMessage = false;
                        EmailValidationMessage = string.Empty;
                    }
                    PropertyChangedAction(Constants.Page.ShowEmailValidationMessage);
                    PropertyChangedAction(Constants.Page.EmailValidationMessage);
                    break;
                case Constants.Page.Password:
               
                    if (passwordValidationRules.Any(x => !x.IsRuleValid(password)))
                    {
                        var errorMsg = passwordValidationRules
                            .FirstOrDefault(x => !x.IsRuleValid(password)).ValidationMessage(password);


                        PasswordValidationMessage = errorMsg;
                        ShowPasswordValidationMessage = true;
                    }
                    else
                    {
                        ShowPasswordValidationMessage = false;
                        PasswordValidationMessage = string.Empty;
                    }
                    PropertyChangedAction(Constants.Page.ShowPasswordValidationMessage);
                    PropertyChangedAction(Constants.Page.PasswordValidationMessage);
                    break;

                case Constants.Page.FirstName:
             
                    if (firstValidationRules.Any(x => !x.IsRuleValid(FirstName)))
                    {
                        var errorMsg = firstValidationRules
                            .FirstOrDefault(x => !x.IsRuleValid(FirstName)).ValidationMessage(FirstName);
                        FirstNameValidationMessage = errorMsg;
                        ShowFirstNameValidationMessage = true;
                    }
                    else
                    {
                        FirstNameValidationMessage = string.Empty;
                        ShowFirstNameValidationMessage = false;
                    }
                    PropertyChangedAction(Constants.Page.ShowFirstNameValidationMessage);
                    PropertyChangedAction(Constants.Page.FirstNameValidationMessage);
                    break;
                default: break;
            }
        }
    }
}
