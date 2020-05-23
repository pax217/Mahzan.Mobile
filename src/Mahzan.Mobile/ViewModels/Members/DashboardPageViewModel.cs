using Mahzan.Mobile.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members
{
    public class DashboardPageViewModel : ViewModelBase
    {
        private readonly IBlueToothService _blueToothService;

        private IList<string> _deviceList;
        public IList<string> DeviceList
        {
            get
            {
                if (_deviceList == null)
                    _deviceList = new ObservableCollection<string>();
                return _deviceList;
            }
            set
            {
                _deviceList = value;
            }
        }

        private string _printMessage;
        public string PrintMessage
        {
            get
            {
                return _printMessage;
            }
            set
            {
                _printMessage = value;
            }
        }

        private string _selectedDevice;
        public string SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
            }
        }

        /// <summary>
        /// Print text-message
        /// </summary>
        public ICommand PrintCommand => new Command(async () =>
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("           - Mahzan -          \n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("Cerrada del Jaguey N.1         \n");
            stringBuilder.Append("San Andres Totoltepec          \n");
            stringBuilder.Append("Tlalpan,CDMX,C.P. 14400        \n");
            stringBuilder.Append("Tel. +52 1 55 2020 5008        \n");
            stringBuilder.Append("www.mahzan.com                 \n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("Descripcion        C.    Precio\n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("Stefano Cosmo 159ml            \n");
            stringBuilder.Append("                   1      53.45\n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("Lubriderm Piel Normal          \n");
            stringBuilder.Append("                   1      86.20\n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("              Subotal    109.65\n");
            stringBuilder.Append("                  IVA      8.55\n");
            stringBuilder.Append("                Total    118.20\n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("CIENTO DIEZ Y OCHO PESOS 20    \n");
            stringBuilder.Append("/100MN                         \n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("EFECTIVO                       \n");
            stringBuilder.Append("                Pago     200.00\n");
            stringBuilder.Append("                Cambio    81.80\n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("# ARTICULOS VENDIDOS       2   \n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("     ***COPIA DE CLIENTE***    \n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append(" *** Gracias por su compra *** \n");
            stringBuilder.Append("-------------------------------\n");

            await _blueToothService.Print(SelectedDevice, stringBuilder.ToString());
        });

        public DashboardPageViewModel(
            INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Dashboard";

            _blueToothService = DependencyService.Get<IBlueToothService>();
            //BindDeviceList();
        }

        /// <summary>
        /// Get Bluetooth device list with DependencyService
        /// </summary>
        void BindDeviceList()
        {
            var list = _blueToothService.GetDeviceList();
            DeviceList.Clear();
            foreach (var item in list)
                DeviceList.Add(item);
        }
    }
}
