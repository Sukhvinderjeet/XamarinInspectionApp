namespace CompanyX.Common
{
    public static class Constants
    {


        public static class Headers
        {
            public const string AuthorizationKey = "Authorization";
            public const string AuthorizationValueFormat = "Bearer {0}";
            public const string AcceptKey = "Accept";
            public const string ContentType = "ContentType";
        }
        public static class Page
        {
            public const string Email = "Email";
            public const string EmailValidationMessage = "EmailValidationMessage";
            public const string PasswordValidationMessage = "PasswordValidationMessage";
            public const string Inspection = "Inspection";
            public const string Password = "Password";
            public const string Pending = "Pending";
            public const string ShowEmailValidationMessage = "ShowEmailValidationMessage";
            public const string ShowPasswordValidationMessage = "ShowPasswordValidationMessage";
            public const string IsWorking = "IsWorking";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string FirstNameValidationMessage = "FirstNameValidationMessage";
            public const string ShowFirstNameValidationMessage = "ShowFirstNameValidationMessage";
            public const string ShowStartInspectionButton = "ShowStartInspectionButton";

        }
        public static class ValidationMessage
        {
            public const string InvalidEmailError = "Please provide a valid Email Id";
            public const string ValueCanNotBeNullError = "This is a required field and cannot be left blank";
            public const string PasswordCanNotBeNullError = "Password cannot be left blank";
            public const string PasswordLengthError = "Password length must be between 6-15";
        }
        public static class ServerUri
        {
            public const string BaseUrl = "https://sukhnagpxamarin.azurewebsites.net/";
            public const string Login = "api/login";
        }

        public static class User
        {
            public const string AuthorizationToken = "AuthorizationToken";
            public const string IsUserLoggedId = "IsUserLoggedId";
            public const string LoggedInUserId = "LoggedInUserId";
        }

        public struct ContentType
        {
            public const string TextHtml = "text/html";
            public const string Json = "application/json";
            public const string Xml = "application/xml";
            public const string UrlEncoded = "application/x-www-form-urlencoded";
            public const string Pdf = "application/pdf";
        }

        public static class HeadersConstants
        {
            public const string SecurityToken = "X-Security-Token";
            public const string JsonContentType = "application/json;profile=\"https://en.wikipedia.org/wiki/PascalCase\"";
            public const string ContentType = "Content-Type";
            public const string Accept = "Accept";
            public const string RequestId = "RequestId";
            public const string UICulture = "X-UICulture";
            public const string Culture = "X-Culture";
            public const string RefreshToken = "X-Refresh-Token";
            public const string XmlContentType = "application/xml";
        }
    }
}
