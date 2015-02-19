
using Android.App;
using Android.Views;
using Android.OS;
using Android.Webkit;

namespace WM.Droid
{
	[Activity (Label = "Water meter", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
	public class MainActivity : Activity
	{
		WebView webview;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			webview = FindViewById<WebView> (Resource.Id.webview);
			webview.SetWebViewClient (new WebViewClient ());
			webview.Settings.JavaScriptEnabled = true;
			webview.Settings.LoadWithOverviewMode = true;
			webview.Settings.UseWideViewPort = true;
			webview.Settings.BuiltInZoomControls = true;
			webview.LoadUrl ("http://pampon.info-lan.me/watermeter");
		}

		public override bool OnKeyDown (Keycode keyCode, KeyEvent e)
		{
			if (keyCode == Keycode.Back && webview.CanGoBack ()) {
				webview.GoBack ();
				return true;
			}

			return base.OnKeyDown (keyCode, e);
		}
	}
}


