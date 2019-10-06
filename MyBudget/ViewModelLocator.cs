using MyBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget
{
    public class ViewModelLocator
    {
        private static ExpenseListViewModel _expenseListViewModel = new ExpenseListViewModel();
        private static ExpenseChartsViewModel expenseChartsViewModel = new ExpenseChartsViewModel();
        public static ExpenseListViewModel ExpenseListViewModel
        {
            get
            {
                return _expenseListViewModel;
            }
        }
        public static ExpenseChartsViewModel ExpenseChartsViewModel
        {
            get
            {
                return expenseChartsViewModel;
            }
        }
    }
}
