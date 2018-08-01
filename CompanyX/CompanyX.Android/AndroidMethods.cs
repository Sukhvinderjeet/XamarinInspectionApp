using CompanyX.Common;
using CompanyX.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace CompanyX.Droid
{
    public class AndroidMethods : IAndroidMethods
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }

}