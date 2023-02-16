using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Xam.Netina.DatePicker.Forms.Models.ItemModels
{
    public class NetinaYearItemModel : INotifyPropertyChanged
    {
        private int _year;
        public int Year
        {
            get { return _year; }
            set { _year = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Year))); }
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
