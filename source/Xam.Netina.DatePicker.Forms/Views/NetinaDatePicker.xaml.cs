using MD.PersianDateTime.Standard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Netina.DatePicker.Forms.Models.ItemModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xam.Netina.DatePicker.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NetinaDatePicker : ContentView
    {

        private ObservableCollection<NetinaDayItemModel> days = new ObservableCollection<NetinaDayItemModel>();
        private ObservableCollection<NetinaMonthItemModel> months = new ObservableCollection<NetinaMonthItemModel>();
        private ObservableCollection<NetinaYearItemModel> years = new ObservableCollection<NetinaYearItemModel>();
        private NetinaDayItemModel selectedDay = null;
        private NetinaMonthItemModel selectedMonth = null;
        private NetinaYearItemModel selectedYear = null;
        private PersianDateTime calenderDate = PersianDateTime.Today;
        public event EventHandler<DateTime> DateTimeSelected;
        public event EventHandler<NetinaDayItemModel> DateTimeSubmited;
        public event EventHandler CloseSubmited;

        private bool _showButtons = false;
        public bool ShowButtons
        {
            get => _showButtons;
            set
            {
                _showButtons = value;
                if(_showButtons)
                    buttonGrid.IsVisible = true;
                else
                    buttonGrid.IsVisible = false;
            }
        }

        public NetinaDatePicker()
        {
            InitializeComponent();
            nextMonthBtn.Text = "<";
            pervMonthBtn.Text = ">";
            for (int i = 0; i < 37; i++)
                days.Add(new NetinaDayItemModel());
            for (int i = 1; i <= 12; i++)
            {
                PersianDateTime dateTime = new PersianDateTime(1, i, 1);
                months.Add(new NetinaMonthItemModel { MonthIndex = i, MonthName = dateTime.MonthName });
            }
            for (int i = (PersianDateTime.Now).Year + 10 ; i >= 1300; i--)
            {
                years.Add(new NetinaYearItemModel { Year = i });
            }
            monthsCollectionView.ItemsSource = months;
            yearsCollectionView.ItemsSource = years;
            daysCollectionView.ItemsSource = days;
            Init(PersianDateTime.Today);
        }

        private void Init(PersianDateTime initDate)
        {
            PersianDateTime selectedPersianDate = initDate;
            monthLable.Text = selectedPersianDate.MonthName;
            yearLable.Text = selectedPersianDate.Year.ToString();
            var monthItem = months.FirstOrDefault(m => m.MonthIndex == selectedPersianDate.Month);
            if (selectedMonth != null)
                selectedMonth.IsSelected = false;
            selectedMonth = monthItem;
            selectedMonth.IsSelected = true;
            var yearItem = years.FirstOrDefault(y => y.Year == selectedPersianDate.Year);
            if (selectedYear != null)
                selectedYear.IsSelected = false;
            selectedYear = yearItem;
            selectedYear.IsSelected = true;
            var firstMonth = selectedPersianDate.AddDays(-(selectedPersianDate.Day - 1));
            var dayOfWeek = (int)firstMonth.DayOfWeek;
            dayOfWeek = (dayOfWeek + 1) % 7;
            int dayIndex = 0;
            for (int i = dayOfWeek; i > 0; i--, dayIndex++)
            {
                var day = firstMonth.AddDays(-i);
                days[dayIndex].DateTime = day.ToDateTime();
                days[dayIndex].IsEnable = false;
                days[dayIndex].IsToday = false;
                days[dayIndex].Day = day.Day;
                days[dayIndex].PersianDateTime = day;
            }
            for (int i = 0; i < selectedPersianDate.GetMonthDays; i++, dayIndex++)
            {
                var day = firstMonth.AddDays(i);
                var dayItem = new NetinaDayItemModel() { IsEnable = true, IsToday = false, Day = day.Day, PersianDateTime = day, DateTime = day.ToDateTime() };
                if (day.Date == PersianDateTime.Today)
                {
                    if (selectedDay != null)
                        selectedDay.IsSelected = false;
                    dayItem.IsSelected = true;
                    dayItem.IsToday = true;
                    selectedDay = dayItem;
                    
                }
                days[dayIndex].DateTime = dayItem.DateTime;
                days[dayIndex].IsEnable = dayItem.IsEnable;
                days[dayIndex].Day = dayItem.Day;
                days[dayIndex].IsToday = dayItem.IsToday;
                days[dayIndex].PersianDateTime = dayItem.PersianDateTime;
            }
            for (int i = 0; dayIndex < 37; i++, dayIndex++)
            {
                var day = firstMonth.AddMonths(1).AddDays(i);

                days[dayIndex].DateTime = day.ToDateTime();
                days[dayIndex].IsEnable = false;
                days[dayIndex].IsToday = false;
                days[dayIndex].Day = day.Day;
                days[dayIndex].PersianDateTime = day;

            }
        }

        private void NetinaDayItemTemplate_Selected(object sender, NetinaDayItemModel e)
        {
            if (selectedDay != null)
                selectedDay.IsSelected = false;
            selectedDay = e;
            selectedDay.IsSelected = true;

            DateTimeSelected?.Invoke(this, e.DateTime);
        }

        private void NetinaMonthItemTemplate_Selected(object sender, NetinaMonthItemModel e)
        {
            if (selectedMonth != null)
                selectedMonth.IsSelected = false;
            selectedMonth = e;
            selectedMonth.IsSelected = true;
            calenderDate = new PersianDateTime(calenderDate.Year, e.MonthIndex , calenderDate.Day);
            Init(calenderDate);
            monthGrid.IsVisible = false;
        }

        private void NetinaYearItemTemplate_Selected(object sender, NetinaYearItemModel e)
        {
            if (selectedYear != null)
                selectedYear.IsSelected = false;
            selectedYear = e;
            selectedYear.IsSelected = true;
            calenderDate = new PersianDateTime(e.Year, calenderDate.Month, calenderDate.Day);
            Init(calenderDate);
            yearGrid.IsVisible = false;
        }

        private void nextMonthBtn_Clicked(object sender, EventArgs e)
        {
            if (selectedDay != null)
                selectedDay.IsSelected = false;
            calenderDate = calenderDate.AddMonths(1);
            Init(calenderDate);
        }

        private void pervMonthBtn_Clicked(object sender, EventArgs e)
        {
            if (selectedDay != null)
                selectedDay.IsSelected = false;
            calenderDate = calenderDate.AddMonths(-1);
            Init(calenderDate);

        }

        private void monthSection_Tapped(object sender, EventArgs e)
        {
            monthGrid.IsVisible = true;
        }

        private void monthClose_Clicked(object sender, EventArgs e) => monthGrid.IsVisible = false;

        private void yearSection_Tapped(object sender, EventArgs e)
        {
            yearGrid.IsVisible = true;
        }

        private void yearClose_Clicked(object sender, EventArgs e) => yearGrid.IsVisible = false;

        private void SubmitDateButton_Clicked(object sender, EventArgs e)
        {
            DateTimeSubmited?.Invoke(this, selectedDay);
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            CloseSubmited?.Invoke(this, EventArgs.Empty);
        }

        private void yearMonthChange_Clicked(object sender, EventArgs e)
        {
            if(sender is Button btn)
            {
                if(btn.Text== "انتخابــ سال")
                {
                    yearGrid.IsVisible = true;
                    monthGrid.IsVisible = false;
                }
                if (btn.Text == "انتخابــ ماه")
                {
                    yearGrid.IsVisible = false;
                    monthGrid.IsVisible = true;
                }
            }
        }
    }
}