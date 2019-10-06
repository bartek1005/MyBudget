using MyBudget.DAL;
using MyBudget.Model;
using MyBudget.Services;
using MyBudget.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBudget.ViewModel
{
    public class ExpenseChartsViewModel
    {
        private IExpenseRepository repository = new ExpenseRepository();
        private List<Expense> allExpenses;
        private ObservableCollection<Expense> expenses;
        private ObservableCollection<KeyValuePair<DateTime, double>> groupedExpenses;

        public ExpenseChartsViewModel()
        {
            allExpenses = repository.GetExpenses();
            groupedExpenses = new ObservableCollection<KeyValuePair<DateTime, double>>(GetGroupedExpenses());
        }

        public ObservableCollection<Expense> Expenses
        {
            get
            {
                return new ObservableCollection<Expense>(allExpenses);
            }
        }

        private List<KeyValuePair<DateTime, double>> GetGroupedExpenses()
        {
            List<KeyValuePair<DateTime, double>> dailyExpensesList = new List<KeyValuePair<DateTime, double>>();
            List<DateTime> dates = new List<DateTime>();
            DateTime startDate = allExpenses.Min(s => s.ExpenseDateTime).Date;
            DateTime endDate = allExpenses.Max(s => s.ExpenseDateTime).Date;

            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(1))
                dates.Add(dt);

            double dailyExpenses = 0;

            foreach (var d in dates)
            {
                dailyExpenses = allExpenses.Where(s => s.ExpenseDateTime.Day == d.Day).Sum(s => s.Price);
                dailyExpensesList.Add(new KeyValuePair<DateTime, double>(d, dailyExpenses));
            }

            return dailyExpensesList;
        }
        public ObservableCollection<KeyValuePair<DateTime, double>> ExpensesGroupedByDate {
            get
            {
                return groupedExpenses;
            }
        }

    }
}
