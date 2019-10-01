using MyBudget.DAL;
using MyBudget.Model;
using MyBudget.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyBudget.ViewModel
{
    public class ExpenseListViewModel : INotifyPropertyChanged
    {
        private List<Expense> allExpenses;
        private ObservableCollection<Expense> expenses;
        private string searchInput;
        private IExpenseRepository expenseRepository;

        private ExpenseType selectedExpenseType;
        public readonly ExpenseType defaultExpenseType = ExpenseType.All;

        private double sum;

        #region Properties
        public CustomCommand ClearFilterCommand { get; private set; }
        public CustomCommand ClearTypeFilterCommand { get; private set; }

        public ExpenseListViewModel()
        {
            expenseRepository = new ExpenseRepository();
            LoadData();
            LoadCommands();
        }

        public ObservableCollection<Expense> Expenses
        {
            get
            {
                return expenses;
            }
            set
            {
                expenses = value;
                RaisePropertyChanged("Expenses");
            }
        }

        public string SearchInput
        {
            get { return searchInput; }
            set
            {
                searchInput = value;
                FilterExpenses();
                RaisePropertyChanged("SearchInput");
            }
        }



        public ObservableCollection<ExpenseType> ExpenseTypes
        {
            get
            {
                IList<ExpenseType> exp = expenses.Select(t => t.Type).Distinct().ToList();
                if (!exp.Contains(defaultExpenseType))
                    exp.Add(defaultExpenseType);
                return new ObservableCollection<ExpenseType>(exp.OrderBy(s => s.ToString()));
            }
        }

        public ExpenseType SelectedExpenseType
        {
            get
            {
                return selectedExpenseType;
            }
            set
            {
                selectedExpenseType = value;
                FilterExpenses();
                RaisePropertyChanged("SelectedExpenseType");
            }
        }

        public double Sum
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value;
                RaisePropertyChanged("Sum");
            }
        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadData()
        {
            allExpenses = expenseRepository.GetExpenses();
            Expenses = new ObservableCollection<Expense>(allExpenses);
            Sum = expenseRepository.GetTotalSum(Expenses.ToList());
        }

        private void FilterExpenses()
        {
            Expenses = expenseRepository.FilterExpenses(allExpenses, searchInput, selectedExpenseType);
            Sum = expenseRepository.GetTotalSum(Expenses.ToList());
        }
        #region ICommand

        public void LoadCommands()
        {
            ClearFilterCommand = new CustomCommand(Clear, CanClear);
            ClearTypeFilterCommand = new CustomCommand(ClearType, CanClearType);
        }

        private void ClearType(object obj) => SelectedExpenseType = defaultExpenseType;

        private bool CanClearType(object obj)
        {
            if (SelectedExpenseType != defaultExpenseType)
                return true;
            return false;
        }

        private bool CanClear(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchInput))
                return false;
            return true;
        }
        private void Clear(object obj) => SearchInput = null;
        #endregion


    }
}
