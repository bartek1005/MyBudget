﻿using MyBudget.DAL;
using MyBudget.Model;
using MyBudget.Services;
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

        private int _tabControlTopSelectedIndex = 0;


        private List<Expense> allExpenses;
        private ObservableCollection<Expense> expenses;
        private string searchInput;
        private IExpenseRepository expenseRepository;
        private DialogService dialogService = new DialogService();

        private Expense _expense;
        private Expense _editExpense;
        private ExpenseType selectedExpenseType;
        public readonly ExpenseType defaultExpenseType = ExpenseType.All;
        private List<Country> _countryList;

        private double sum;



        #region Properties

        public int TabControlTopSelectedIndex
        {
            get { return _tabControlTopSelectedIndex; }
            set
            {
                _tabControlTopSelectedIndex = value;
                SetEditedExpense();
                RaisePropertyChanged("TabControlTopSelectedIndex");
            }
        }


        public CustomCommand ClearFilterCommand { get; private set; }
        public CustomCommand ClearTypeFilterCommand { get; private set; }
        public CustomCommand EditExpenseCommand { get; private set; }
        public CustomCommand UpdateExpenseCommand { get; private set; }
        public CustomCommand OpenNewWindowCommand { get; private set; }
        public CustomCommand AddExpenseCommand { get; private set; }

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

        public Expense SelectedExpense
        {
            get
            {
                return _expense;
            }
            set
            {
                _expense = value;
                SetEditedExpense();
                RaisePropertyChanged("SelectedExpense");
            }
        }

        private void SetEditedExpense()
        {
              EditedExpense = SelectedExpense;
          //  EditedExpense.Price = 1;
        }

        public Expense EditedExpense
        {
            get { return _editExpense; }
            set
            {
                _editExpense = value;
                RaisePropertyChanged("EditedExpense");
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

        public List<Country> CountryList
        {
            get{ return _countryList; }
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
            _countryList = expenseRepository.GetCountries();
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
            EditExpenseCommand = new CustomCommand(EditExpense, CanEdit);
            UpdateExpenseCommand = new CustomCommand(UpdateExpense, CanUpdate);
            OpenNewWindowCommand = new CustomCommand(OpenNewWindow, CanOpenNewWindow);
            AddExpenseCommand = new CustomCommand(OnAdd);
        }

        private void OnAdd(object obj)
        {
            Expense newExpense = new Expense();
            SelectedExpense = newExpense;
            TabControlTopSelectedIndex = 1;
        }

        private void UpdateExpense(object obj)
        {
            expenseRepository.UpdateExpense(EditedExpense);
            LoadData();
            TabControlTopSelectedIndex = 0;
        }

        private bool CanUpdate(object obj)
        {
            return true;
        }

        private void EditExpense(object obj)
        {
            TabControlTopSelectedIndex = 1;
        }

        private bool CanEdit(object obj)
        {
            return SelectedExpense != null ? true : false;
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
        #region Navigation

        private void OpenNewWindow(object obj)
        {
            dialogService.ShowCharts();
        }

        private bool CanOpenNewWindow(object obj)
        {
            return true;
        }

        #endregion


    }
}
