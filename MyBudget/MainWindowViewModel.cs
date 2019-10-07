using MyBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget
{
    public class MainWindowViewModel
    {
        public object CurrentViewModel { get; set; }
        public MainWindowViewModel()
        {
            CurrentViewModel = new ExpenseAllViewModel();
        }
    }
}
