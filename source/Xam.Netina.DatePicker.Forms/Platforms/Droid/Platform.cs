using Android.Content;
using Rg.Plugins.Popup;

namespace Xam.Netina.DatePicker.Forms.Platforms.Droid
{
    public static class Platform
    {
        public static void Init(Context context)
        {
            Popup.Init(context);
        }
    }
}
