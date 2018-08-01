using CompanyX.Common;
using CompanyX.Common.Model;
using System.Threading.Tasks;

namespace CompanyX.Manager
{
    public interface IUserManager
    {
        void LogIn(LoginRequestModel loginRequestModel);

        void SignUp(SignUpRequestModel signUpRequestModel);
        void LogOut();
    }
}