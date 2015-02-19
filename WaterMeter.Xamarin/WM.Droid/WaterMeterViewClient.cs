using System;
using Android.Webkit;

namespace WM.Droid
{
	public class WaterMeterViewClient: WebViewClient
	{
		public override bool ShouldOverrideUrlLoading (WebView view, string url)
		{
			view.LoadUrl (url);
			return true;
		}
	}
}

