using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mahzan.Mobile.Behaviors
{
    public class PickerTaxConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var type = value.ToString();

            if (type != "")
            {
                return "Correcto";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
