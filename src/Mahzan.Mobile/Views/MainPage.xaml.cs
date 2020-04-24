using Mahzan.Mobile.Views.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mahzan.Mobile.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public List<MahzanMenu> MahzanMenu { get; set; }

        public MainPage()
        {
            InitializeComponent();

        }

    }
}