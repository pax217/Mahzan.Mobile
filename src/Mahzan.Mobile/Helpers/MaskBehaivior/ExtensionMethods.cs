using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mahzan.Mobile.Helpers.MaskBehaivior
{
    public static class ExtensionMethods
    {
        public static string RemoveNonNumbers(this string texto)
        {
            var regex = new Regex(@"[^\d]");
            return regex.Replace(texto, "");
        }
    }
}
