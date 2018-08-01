namespace CompanyX.ValidationRule
{
    using CompanyX.Common;
    using System.Text.RegularExpressions;
    public class EmaiValidationRule : IValidationRule<string>
    {
        private string validationMessage { get; set; }

        public bool IsRuleValid(string value)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {

                isValid = false;
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value);
                if (!match.Success)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
        public string ValidationMessage(string value)
        {

            if (string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value))
            {
                validationMessage = Constants.ValidationMessage.ValueCanNotBeNullError;

            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value);
                if (!match.Success)
                {
                    validationMessage = Constants.ValidationMessage.InvalidEmailError;
                }
            }
            return validationMessage;
        }
    }
}
