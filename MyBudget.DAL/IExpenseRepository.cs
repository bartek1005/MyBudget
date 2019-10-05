using MyBudget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyBudget.DAL
{
    public interface IExpenseRepository
    {
        void DeleteExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void AddExpense(Expense expense);
        List<Expense> GetExpenses();
        List<Expense> GetExpensesByType(string expenseType);
        List<Expense> GetExpensesByCountry(string countryName);
        List<Expense> GetExpensesFromTo(DateTime startDate, DateTime endDate);
        List<ExpenseType> GetExpenseTypes();
        ObservableCollection<Expense> FilterExpenses(IList<Expense> expenses, string searchInput, ExpenseType expenseType);

        #region Country
        List<Country> GetCountries();
        #endregion
        #region Math
        double GetTotalSum(List<Expense> expenses);
        double GetMaxPrice(List<Expense> expenses);
        double GetAveragePrice(List<Expense> expenses);
        double GetAverageRate(List<Expense> expenses);
        #endregion
    }
}
