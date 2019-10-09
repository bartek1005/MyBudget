using MyBudget.DAL;
using MyBudget.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.ViewModel
{
    public class ExpenseAllViewModel : BindableBase
    {
        private IExpenseRepository _repository;
        private List<Expense> _allExpenses;

        public ExpenseAllViewModel()
        {
            _repository = new ExpenseRepository();
            _allExpenses = _repository.GetExpenses();
            Expenses = new ObservableCollection<Expense>(_allExpenses);
            EditCommand = new RelayCommand(OnEdit, CanEdit);
        }

        #region Properties

        private int _tabControlSelectedIndex;

        public int TabControlSelectedIndex
        {
            get { return _tabControlSelectedIndex; }
            set { SetProperty(ref _tabControlSelectedIndex, value); }
        }

        private ObservableCollection<Expense> _expenses;
        public ObservableCollection<Expense> Expenses
        {
            get { return _expenses; }
            set { SetProperty(ref _expenses, value); }
        }

        private Expense _selectedExpense;

        public Expense SelectedExpense
        {
            get { return _selectedExpense; }
            set
            {
                SetProperty(ref _selectedExpense, value);
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private ExpenseType _selectedExpenseType;

        public ExpenseType SelectedExpenseType
        {
            get { return _selectedExpenseType; }
            set { SetProperty(ref _selectedExpenseType, value); }
        }


        private string _searchInput;

        public string SearchInput
        {
            get { return _searchInput; }
            set
            {
                SetProperty(ref _searchInput, value);
                FilterExpenses();
            }
        }

        public RelayCommand EditCommand { get; private set; }
        #endregion

        #region Commands
        private void OnEdit()
        {
            TabControlSelectedIndex = 1;
        }

        private bool CanEdit()
        {
            return SelectedExpense != null;
        }
        #endregion

        #region Methods
        private void FilterExpenses()
        {
            Expenses = FilterExpenses(Expenses, SearchInput, SelectedExpenseType);
        }

        public ObservableCollection<Expense> FilterExpenses(IList<Expense> expenses, string searchInput, ExpenseType selectedExpenseType)
        {
            ObservableCollection<Expense> newExpenses = new ObservableCollection<Expense>(expenses);

            if (selectedExpenseType != ExpenseType.All)
                newExpenses = new ObservableCollection<Expense>(newExpenses.Where(e => e.Type == selectedExpenseType));


            if (!string.IsNullOrWhiteSpace(searchInput))
                newExpenses = new ObservableCollection<Expense>(newExpenses.Where(e => e.ProductName.ToLower().Contains(searchInput.ToLower())));

            return newExpenses;

        }
        #endregion
    }
}