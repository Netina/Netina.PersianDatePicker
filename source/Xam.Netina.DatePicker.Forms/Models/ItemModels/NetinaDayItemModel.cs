using MD.PersianDateTime.Standard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Xam.Netina.DatePicker.Forms.Models.ItemModels
{

    public class NetinaDayItemModel : INotifyPropertyChanged
    {

        private PersianDateTime _persianDateTime;
        public PersianDateTime PersianDateTime
        {
            get => _persianDateTime;
            set { _persianDateTime = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PersianDateTime))); }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set { _dateTime = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTime))); }
        }

        private int _day;
        public int Day
        {
            get => _day;
            set { _day = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Day))); }
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set { _isEnable = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnable))); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected))); }
        }

        private bool _isToday;
        public bool IsToday
        {
            get => _isToday;
            set { _isToday = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsToday))); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
