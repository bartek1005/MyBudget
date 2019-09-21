using MyBudget.DAL;
using MyBudget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.ViewModel
{
    public class ExpenseListViewModel : INotifyPropertyChanged
    {
        private List<Expense> allExpenses;
        private ObservableCollection<Expense> expenses;
        private string searchInput;
        private IExpenseRepository expenseRepository;
        public ExpenseListViewModel()
        {
            expenseRepository = new ExpenseRepository();
            LoadData();
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
                FilterExpenses(searchInput);
            }
        }

        private void FilterExpenses(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
                Expenses = new ObservableCollection<Expense>(allExpenses);
            else
                Expenses = new ObservableCollection<Expense>(allExpenses.Where(e => e.ProductName.ToLower().Contains(searchInput.ToLower())));
        }

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

        }
    }
}
