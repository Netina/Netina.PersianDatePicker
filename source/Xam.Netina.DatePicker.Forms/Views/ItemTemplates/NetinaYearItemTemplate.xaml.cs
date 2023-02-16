using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Netina.DatePicker.Forms.Models.ItemModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xam.Netina.DatePicker.Forms.Views.ItemTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetinaYearItemTemplate : ContentView
    {
        public event EventHandler<NetinaYearItemModel> Selected;

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(
                propertyName: nameof(IsSelected),
                returnType: typeof(bool),
                declaringType: typeof(NetinaYearItemTemplate),
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
            var viewModel = bindable as NetinaYearItemTemplate;
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
        public NetinaYearItemTemplate()
        {
            InitializeComponent();
        }
        private void yearItem_Tapped(object sender, EventArgs e)
        {

            if (BindingContext is NetinaYearItemModel itemModel)
                Selected.Invoke(this, itemModel);
        }
    }
}