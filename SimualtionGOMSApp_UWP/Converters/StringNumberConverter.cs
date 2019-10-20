using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SimualtionGOMSApp_UWP.Converters
{
    class StringNumberConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if(typeof(int) == targetType)
            {
                if (!string.IsNullOrEmpty((string)value) && int.TryParse((string)value, out var temp))
                    return temp;
                return 0;
            }

            if(typeof(double) == targetType)
            {
                if (!string.IsNullOrEmpty((string)value) && double.TryParse((string)value, out var temp))
                    return temp;

                return 0;
            }

            return 0;
        }
    }
}
