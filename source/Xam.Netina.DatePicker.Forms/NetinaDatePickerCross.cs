using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xam.Netina.DatePicker.Forms.Views;

namespace Xam.Netina.DatePicker.Forms
{
    public class NetinaDatePickerCross
    {
        private static NetinaDatePickerCross _current;
        public static NetinaDatePickerCross Current 
        {
            get
            {
                if(_current==null)
                    _current = new NetinaDatePickerCross();
                return _current;    
            }
        }
        public NetinaDatePickerCross()
        {
            
        }

        public async Task PushDatePickerPopup(Action<DateTime> dateTimeSelectAction)
        {
            var popUp = new NetinaDatePicker { ShowButtons = true };
            popUp.DateTimeSubmited += (ss, ee) =>
            {
                PopupNavigation.Instance.PopAsync();
                dateTimeSelectAction?.Invoke(ee.DateTime);
            };
            popUp.CloseSubmited += (ss, ee) =>
            {
                PopupNavigation.Instance.PopAsync();
            };
            await PopupNavigation.Instance.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage { Content = popUp });
        }
    }
}
