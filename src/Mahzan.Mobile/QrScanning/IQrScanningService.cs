using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.QrScanning
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
