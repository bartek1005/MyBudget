using MyBudget.Model;
using System;
using System.Collections.Generic;
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
    }
}
