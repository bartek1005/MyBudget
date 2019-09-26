using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MyBudget.Model;

namespace MyBudget.DAL
{
    public class ExpenseRepository : IExpenseRepository
    {
        private static List<Expense> expenses;
        public ExpenseRepository()
        {

        }

        public void AddExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        public void DeleteExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Expense> FilterExpenses(IList<Expense> expenses, string searchInput, ExpenseType selectedExpenseType)
        {
            ObservableCollection<Expense> Expenses = new ObservableCollection<Expense>(expenses);

            if (selectedExpenseType != ExpenseType.All)
                Expenses = new ObservableCollection<Expense>(Expenses.Where(e => e.Type == selectedExpenseType));

            if (!string.IsNullOrWhiteSpace(searchInput))
                Expenses = new ObservableCollection<Expense>(Expenses.Where(e => e.ProductName.ToLower().Contains(searchInput.ToLower())));

            return Expenses;

        }
        public List<Expense> GetExpenses()
        {
            if (expenses == null)
                LoadExpenses();
            return expenses;
        }

        public List<Expense> GetExpensesByCountry(string countryName)
        {
            if (expenses == null)
                LoadExpenses();
            return expenses.Where(e => e.Country.ToString() == countryName).ToList();
        }

        public List<Expense> GetExpensesByType(string expenseType)
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetExpensesFromTo(DateTime startDate, DateTime endDate)
        {
            if (expenses == null)
                LoadExpenses();
            return expenses.Where(d => d.ExpenseDateTime >= startDate && d.ExpenseDateTime <= endDate).ToList();
        }

        public List<ExpenseType> GetExpenseTypes()
        {
            return expenses.Select(t => t.Type).Distinct().ToList();
        }



        public void UpdateExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        private double ConvertPriceToPLN(double price, Country country)
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var currentRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(country.ToString()) || region.ThreeLetterISORegionName.Contains(country.ToString()));
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
            return Math.Round(price, 2);
        }

        public double GetTotalSum(ObservableCollection<Expense> expenses)
        {
            double sum = 0;
            foreach (var expense in expenses)
            {
                sum += ConvertPriceToPLN(expense.Price, expense.Country);
            }
            return Math.Round(sum,2);
        }

        private void LoadExpenses()
        {
            expenses = new List<Expense>()
            {
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-20 12:40"),
                   ProductName = "Curry Chicken Croissant",
                   Price = 10.10,
                   Country = Country.USA,
                   Type = ExpenseType.Lunch,
                   Rate = 5
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-21 13:50"),
                   ProductName = "Carnitas a la Mexicana",
                   Price = 10.62,
                   Country = Country.USA,
                   Type = ExpenseType.Lunch,
                   Rate = 5
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Peaches",
                   Price = 2.16,
                   Country = Country.USA,
                   Type = ExpenseType.Fruit,
                   Rate = 6
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Bananas",
                   Price = 1.12,
                   Country = Country.USA,
                   Type = ExpenseType.Fruit,
                   Rate = 4,
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Salad",
                   Price = 2.98*1.06,
                   Country = Country.USA,
                   Type = ExpenseType.Food,
                   Rate = 3,
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Yogurt",
                   Price = 0.64*1.06,
                   Country = Country.USA,
                   Type = ExpenseType.Food,
                   Rate = 4
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Yogurts",
                   Price = 2.44*1.06,
                   Country = Country.USA,
                   Type = ExpenseType.Food,
                   Rate = 5
                },
                new Expense()
                {
                    ExpenseDateTime=DateTime.Parse("2019-09-18 18:35"),
                    ProductName="Chips",
                    Price = 1.06,
                    Country = Country.USA,
                    Type = ExpenseType.Chips,
                    Rate = 4
                },
                new Expense()
                {
                    ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                    ProductName="Granola",
                    Price = 3.77*1.06,
                    Country=Country.USA,
                    Type = ExpenseType.Food,
                    Rate = 6
                },
                new Expense()
                {
                    ExpenseDateTime = DateTime.Parse("2019-09-24 18:35"),
                    ProductName="TEST",
                    Price = 11.254,
                    Country=Country.Poland,
                    Type = ExpenseType.Food,
                    Rate = 6
                },
                new Expense()
                {
                    ExpenseDateTime = DateTime.Parse("2019-09-24 20:11"),
                    ProductName="TEST2",
                    Price = 0.23123,
                    Country=Country.Slovakia,
                    Type = ExpenseType.Food,
                    Rate = 0
                }
            };
        }
    }
}
