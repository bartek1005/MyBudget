using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyBudget.Converter
{
    public class PriceToPLNConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double price = 0;
            string country;
            if (values[0] == DependencyProperty.UnsetValue)
                return DependencyProperty.UnsetValue;
            else
                price = Double.Parse(values[0].ToString());

            if (values[1] == DependencyProperty.UnsetValue)
                return DependencyProperty.UnsetValue;
            else
                country = values[1].ToString();

            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var currentRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(country) || region.ThreeLetterISORegionName.Contains(country));
            string currencyName = currentRegion.ISOCurrencySymbol;

            switch (currencyName)
            {
                case "USD":
                    price *= 3.99;
                    break;
                case "EUR":
                    price *= 4.38;
                    break;
                case "PLN":
                default:
                    break;

            }
            return string.Format("{0:F2}",Math.Round(price, 2)) + " zl";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
