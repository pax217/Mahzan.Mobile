using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.SqLite.Entities
{
    public class BluetoothDevice
    {
        [PrimaryKey]
        public string DeviceName { get; set; }
    }
}
