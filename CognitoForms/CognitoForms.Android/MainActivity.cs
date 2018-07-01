using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SaltyDog.CognitoForms.App;
using Plugin.Settings;
using SaltyDog.CognitoForms;

namespace SaltyDog.CognitoForms.Droid
{
	[Activity(Label = "CognitoForms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = SaltyDog.CognitoForms.Droid.Resource.Layout.Tabbar;
			ToolbarResource = SaltyDog.CognitoForms.Droid.Resource.Layout.Toolbar;

			SessionStore.Instance = new SessionStore { Settings = CrossSettings.Current };

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new SaltyDog.CognitoForms.App.App());
		}
	}
}

