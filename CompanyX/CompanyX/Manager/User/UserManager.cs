namespace CompanyX.Manager
{
    using CompanyX.Common;
    using CompanyX.Common.Integration.Provider;
    using CompanyX.Common.Model;
    using Microsoft.AppCenter.Crashes;
    using Plugin.Connectivity;
    using Plugin.SecureStorage;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserManager : IUserManager
    {
        public void LogIn(LoginRequestModel loginRequestModel)
        {
            ValidateNetworkConnectivity();
            var client = new CompanyXHttpClient();
            var signInResponseModel = client.Post<SignInResponseModel>("api/login", loginRequestModel);
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.Headers.AuthorizationKey, string.Format(Constants.Headers.AuthorizationValueFormat, signInResponseModel.auth_token));
            headers.Add(Constants.Headers.ContentType, "application/json");

            var userName = client.Get<string>("api/login", headers);
            CrossSecureStorage.Current.SetValue(Constants.User.IsUserLoggedId, "true");
            CrossSecureStorage.Current.SetValue(Constants.User.AuthorizationToken, signInResponseModel.auth_token);
            CrossSecureStorage.Current.SetValue(Constants.User.LoggedInUserId, userName);
        }


        public void LogOut()
        {
            CrossSecureStorage.Current.DeleteKey(Constants.User.IsUserLoggedId);
            CrossSecureStorage.Current.DeleteKey(Constants.User.AuthorizationToken);
            CrossSecureStorage.Current.DeleteKey(Constants.User.LoggedInUserId);
        }

        /// <summary>
        ///  Network connectivity is required while login
        /// </summary>
        private static void ValidateNetworkConnectivity()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new System.Exception("Please check your network connectivity");
            }
        }

        public void SignUp(SignUpRequestModel signUpRequestModel)
        {
            ValidateNetworkConnectivity();
            var result = new CompanyXHttpClient().Post<SignUpResponseModel>("api/signup", signUpRequestModel);
            if (!result.IsValid)
            {
                throw new System.Exception(result.ErrorMessage);
            }
        }
    }


}
