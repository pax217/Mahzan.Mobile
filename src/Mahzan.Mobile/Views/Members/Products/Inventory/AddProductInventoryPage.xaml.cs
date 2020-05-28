using Xamarin.Forms;

namespace Mahzan.Mobile.Views.Members.Products.Inventory
{
    public partial class AddProductInventoryPage : ContentPage
    {
        public AddProductInventoryPage()
        {
            InitializeComponent();
        }

        private void SwitchInAllStores_Toggled(object sender, ToggledEventArgs e)
        {

            Switch sw = sender as Switch;
            if (sw.IsToggled)
            {
                PickerStores.IsEnabled = false;
                EntryPrice.IsEnabled = false;
                EntryCost.IsEnabled = false;
            }
            else 
            {
                PickerStores.IsEnabled = true;
                EntryPrice.IsEnabled = true;
                EntryCost.IsEnabled = true;
            }
        }
    }
}
