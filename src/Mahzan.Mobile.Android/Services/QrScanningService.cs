using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mahzan.Mobile.QrScanning;
using ZXing.Mobile;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Mahzan.Mobile.Droid.Services.QrScanningService))]
namespace Mahzan.Mobile.Droid.Services
{
    public class QrScanningService: IQrScanningService
    {
        public async Task<string> ScanAsync()
        {

            var optionsCustom = new MobileBarcodeScanningOptions() { };

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Acerca la camara al elemento",
                BottomText = "Toca la pantalla para enfocar",
            };

            var scanResults = await scanner.Scan();

            return scanResults == null ? string.Empty : scanResults.Text;

        }
    }
}