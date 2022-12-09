using SerwisSamochodowy.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SerwisSamochodowy.Converters
{
    internal class MechanikToNazwiskoImieConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value).GetType() == typeof(Mechanik))
                return (value as Mechanik).Nazwisko + " " + (value as Mechanik).Imie;

            return string.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

    }
}


