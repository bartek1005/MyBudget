using MyBudget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyBudget.Converter
{
    public class PriceConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = 0;
            string country;
            if (value[0] == DependencyProperty.UnsetValue)
                return DependencyProperty.UnsetValue;
            else
                price = Decimal.Parse(value[0].ToString());

            if (value[1] == DependencyProperty.UnsetValue)
                return DependencyProperty.UnsetValue;
            else
                country = value[1].ToString();

            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var currentRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(country) || region.ThreeLetterISORegionName.Contains(country));
            string currencySymbol = currentRegion.CurrencySymbol;

            return Math.Round(price,2).ToString() + " " + currencySymbol;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
