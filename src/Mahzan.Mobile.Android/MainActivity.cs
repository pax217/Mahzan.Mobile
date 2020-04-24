using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.Media;
using Prism;
using Prism.Ioc;
using ZXing.Mobile;

namespace Mahzan.Mobile.Droid
{
    [Activity(Label = "Mahzan.Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //-->Initialize scanner
            MobileBarcodeScanner.Initialize(this.Application);

            //--> Incializamos la camara
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            CrossMedia.Current.Initialize();

            Xamarin.Essentials.Platform.Init(this, bundle);

            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

