using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace CuaHangSach.Droid
{
    internal class CustomTabShellItem : ShellItemRenderer
    {
        public CustomTabShellItem(IShellContext shellContext) : base(shellContext)
        {
        }

        /// <summary>
        /// Pops to root when the selected tab is pressed.
        /// </summary>
        /// <param name="shellSection"></param>
        protected override void OnTabReselected(ShellSection shellSection)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                await shellSection?.Navigation.PopToRootAsync();
            });
        }
    }
}