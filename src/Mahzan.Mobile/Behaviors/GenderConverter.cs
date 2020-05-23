using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mahzan.Mobile.Behaviors
{
    class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var gender = value.ToString();
            if (gender == "Male")
                return "You're handsome!";
            else if (gender == "Female")
                return "You're a beauty!";
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
