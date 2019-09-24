using MyBudget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyBudget.Converter
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string usa = "USA";
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var currentRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(usa) || region.ThreeLetterISORegionName.Contains(usa));
            string currencySymbol = currentRegion.CurrencySymbol;

            return value + " " + currencySymbol;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
