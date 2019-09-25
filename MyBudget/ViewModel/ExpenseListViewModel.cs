﻿using MyBudget.DAL;
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

        private ExpenseType selectedExpenseType;
        public readonly ExpenseType defaultExpenseType = ExpenseType.All;

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
                FilterExpenses();
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
            }
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

        private void FilterExpenses()
        {
            Expenses = expenseRepository.FilterExpenses(allExpenses, searchInput, selectedExpenseType);
        }
    }
}
