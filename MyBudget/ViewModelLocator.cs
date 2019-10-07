using MyBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyBudget
{
    public static class ViewModelLocator
    {
        //private static ExpenseListViewModel _expenseListViewModel = new ExpenseListViewModel();
        //private static ExpenseChartsViewModel expenseChartsViewModel = new ExpenseChartsViewModel();
        //public static ExpenseListViewModel ExpenseListViewModel
        //{
        //    get
        //    {
        //        return _expenseListViewModel;
        //    }
        //}
        //public static ExpenseChartsViewModel ExpenseChartsViewModel
        //{
        //    get
        //    {
        //        return expenseChartsViewModel;
        //    }
        //}



        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false,AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;
            var viewType = d.GetType();
            var viewTypeName = viewType.FullName;
            var viewModelTypeName = viewTypeName.Replace("View", "ViewModel");
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
