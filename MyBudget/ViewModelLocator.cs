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
        private static ExpenseListViewModel expenseListViewModel = new ExpenseListViewModel();
        public static ExpenseListViewModel ExpenseListViewModel
        {
            get
            {
                return expenseListViewModel;
            }
        }
    }
}
