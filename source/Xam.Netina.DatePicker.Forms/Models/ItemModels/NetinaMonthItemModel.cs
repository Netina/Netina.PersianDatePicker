using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Xam.Netina.DatePicker.Forms.Models.ItemModels
{
    public class NetinaMonthItemModel : INotifyPropertyChanged
    {
		private string _monthName;
		public string MonthName
		{
			get { return _monthName; }
			set { _monthName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MonthName))); }
		}

		private int _monthIndex;
		public int MonthIndex
		{
			get { return _monthIndex; }
			set { _monthIndex = value; }
		}


        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected))); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
