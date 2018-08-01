using CompanyX.Common;

namespace CompanyX.ValidationRule
{
    public class PasswordValidationRule : IValidationRule<string>
    {
        private string validationMessage;

        public bool IsRuleValid(string value)
        {
            return ((!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value)) && value.Length >= 6);
        }

        public string ValidationMessage(string value)
        {
            if (string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value))
            {
                validationMessage = Constants.ValidationMessage.PasswordCanNotBeNullError;

            }
            else if (value.Length < 6)
            {
                validationMessage = Constants.ValidationMessage.PasswordLengthError;

            }
            return validationMessage;
        }
    }
}
