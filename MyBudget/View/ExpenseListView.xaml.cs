﻿using MyBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace MyBudget.View
{
    /// <summary>
    /// Interaction logic for ExpenseListView.xaml
    /// </summary>
    public partial class ExpenseListView : MetroWindow
    {
        public ExpenseListView()
        {
            //this.DataContext = new ExpenseListViewModel();
            InitializeComponent();
        }
    }
}
