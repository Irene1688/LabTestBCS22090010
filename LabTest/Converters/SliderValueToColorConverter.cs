using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTest.Converters
{
    public class SliderValueToColorConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double sliderValue)
            {
                if (sliderValue < 40) return Colors.Red;
                if (sliderValue < 80) return Colors.Black;
                return Colors.Green;
            }
            return Colors.Black;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
