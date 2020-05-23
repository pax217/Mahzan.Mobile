using Xamarin.Forms;

namespace Mahzan.Mobile.Views.Members.Products.Taxes
{
    public partial class AdminTaxesPage : ContentPage
    {
        public AdminTaxesPage()
        {
            InitializeComponent();
        }


        private void EntryName_Unfocused(object sender, FocusEventArgs e)
        {
            EnableSaveButton();
        }

        private void EntryTextRatePercentage_Unfocused(object sender, FocusEventArgs e)
        {
            EnableSaveButton();
        }

        private void EnableSaveButton()
        {
            if (EntryName.Text != ""
                && EntryName.Text != null
                && EntryTaxRatePercentage.Text != ""
                && EntryTaxRatePercentage.Text != null)
            {
                ButtonSave.IsEnabled = true;
            }
            else
            {
                ButtonSave.IsEnabled = false;
            }


        }
    }
}
