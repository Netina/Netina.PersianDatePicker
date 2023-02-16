using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xam.Netina.DatePicker.Forms.Models.ItemModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xam.Netina.DatePicker.Forms.Views.ItemTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NetinaDayItemTemplate : ContentView
    {
        public event EventHandler<NetinaDayItemModel> Selected;

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(
                propertyName: nameof(IsSelected),
                returnType: typeof(bool),
                declaringType: typeof(NetinaDayItemTemplate),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay,
                null,
                IsSelectedPropertyChanged
            );

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        private static void IsSelectedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var viewModel = bindable as NetinaDayItemTemplate;
            if (newvalue is bool flag)
            {
                if (flag)
                {
                    if (Application.Current.RequestedTheme == OSAppTheme.Dark)
                        viewModel.mainFrame.BackgroundColor = Color.FromHex("#2c3e50");
                    else
                        viewModel.mainFrame.BackgroundColor = Color.FromHex("#FFCA60");
                }
                else
                    viewModel.mainFrame.BackgroundColor = Color.FromHex("#F6F7F6");
            }
        }

        public static readonly BindableProperty IsTodayProperty =
            BindableProperty.Create(
                propertyName: nameof(IsToday),
                returnType: typeof(bool),
                declaringType: typeof(NetinaDayItemTemplate),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay,
                null,
                IsTodayPropertyChanged
            );

        public bool IsToday
        {
            get { return (bool)GetValue(IsTodayProperty); }
            set { SetValue(IsTodayProperty, value); }
        }

        private static void IsTodayPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var viewModel = bindable as NetinaDayItemTemplate;
            if (newvalue is bool flag)
            {

                if (flag)
                {
                    viewModel.mainFrame.BorderColor = Color.FromHex("#5F6386");
                    viewModel.isTodayLabel.IsVisible = true;
                    viewModel.dayLabel.Margin = new Thickness(0, -5, 0, 0);
                }
                else
                {
                    viewModel.isTodayLabel.IsVisible = false;
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(IsEnabled))
            {
                if (!this.IsEnabled)
                    this.Opacity = 0.5;
                else
                    this.Opacity = 1;
            }
            base.OnPropertyChanged(propertyName);
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        public NetinaDayItemTemplate()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (BindingContext is NetinaDayItemModel itemModel)
                Selected.Invoke(this, itemModel);
        }
    }
}