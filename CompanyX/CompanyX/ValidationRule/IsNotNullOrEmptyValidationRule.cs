using CompanyX.Common;

namespace CompanyX.ValidationRule
{
    public class IsNotNullOrEmptyValidationRule : IValidationRule<string>
    {
        private string validationMessage;


        public bool IsRuleValid(string value)
        {
            return (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value));
        }

        public string ValidationMessage(string value)
        {
            if (string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value))
            {
                validationMessage = Constants.ValidationMessage.ValueCanNotBeNullError;

            }
            return validationMessage;
        }
    }
}
