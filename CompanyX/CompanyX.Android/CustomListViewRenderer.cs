using Android.Content;
using CompanyX.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListView), typeof(CustomListViewRenderer))]
namespace CompanyX.Droid
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                // allows ListView to be scrollable inside a of a ScrollView;
                // this behavior is default in iOS so only custom renderer is needed in Android
                var listView = Control as Android.Widget.ListView;
                listView.NestedScrollingEnabled = true;
            }
        }
    }
}