using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyBudget.Model
{
    public class Expense : INotifyPropertyChanged
    {
        private static List<bool> counter = new List<bool>();
        private static readonly int MAX_RATES = 5;
        private static readonly int MIN_RATES = 1;
        private int rate;

        private string _productName;

        public event PropertyChangedEventHandler PropertyChanged;

        public Expense()
        {
            Id = GetId();
        }

        public int Id { get; private set; }
        public DateTime ExpenseDateTime { get; set; }
        public string ProductName { get; set; }
        //{
        //    get { return _productName; }
        //    set { _productName = value; RaisePropertyChanged("ProductName"); }
        //}

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public double Price { get; set; }
        public ExpenseType Type { get; set; }
        public int Rate
        {
            get
            {
                return rate;
            }
            set
            {
                if (value > MAX_RATES)
                    rate = MAX_RATES;
                else if (value < MIN_RATES)
                    rate = MIN_RATES;
                else
                    rate = value;
            }
        }

        public Country Country { get; set; }

        private int GetId()
        {
            int id = 0;
            for (int i = 1; i < counter.Count; i++)
            {
                if (counter[i] == false)
                {
                    id = i;
                    break;
                }
            }

            if (id == 0)
            {
                id = counter.Count + 1;
                counter.Add(true);
            }


            return id;
        }
    }
}
