using System;
using System.Collections.Generic;
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

        public void UpdateExpense(Expense expense)
        {
            throw new NotImplementedException();
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
                   Rate = 4
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-21 13:50"),
                   ProductName = "Carnitas a la Mexicana",
                   Price = 10.62,
                   Country = Country.USA,
                   Type = ExpenseType.Lunch,
                   Rate = 3
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Peaches",
                   Price = 2.16,
                   Country = Country.USA,
                   Type = ExpenseType.Fruit,
                   Rate = 5
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Bananas",
                   Price = 1.12,
                   Country = Country.USA,
                   Type = ExpenseType.Fruit,
                   Rate = 3,
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Salad",
                   Price = 2.98*1.06,
                   Country = Country.USA,
                   Type = ExpenseType.Food,
                   Rate = 2,
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Yogurt",
                   Price = 0.64*1.06,
                   Country = Country.USA,
                   Type = ExpenseType.Food,
                   Rate = 3
                },
                new Expense()
                {
                   ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                   ProductName = "Yogurts",
                   Price = 2.44*1.06,
                   Country = Country.USA,
                   Type = ExpenseType.Food,
                   Rate = 4
                },
                new Expense()
                {
                    ExpenseDateTime=DateTime.Parse("2019-09-18 18:35"),
                    ProductName="Chips",
                    Price = 1.06,
                    Country = Country.USA,
                    Type = ExpenseType.Chips,
                    Rate = 3
                },
                new Expense()
                {
                    ExpenseDateTime = DateTime.Parse("2019-09-18 18:35"),
                    ProductName="Granola",
                    Price = 3.77*1.06,
                    Country=Country.USA,
                    Type = ExpenseType.Food,
                    Rate = 5
                }
            };
        }
    }
}
