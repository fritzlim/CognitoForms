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
using Amazon.CognitoIdentityProvider;
using Amazon;

namespace SaltyDog.CognitoForms.Droid
{
	[Activity(Label = "CognitoForms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        public AmazonCognitoIdentityProviderConfig ClientHttpConfig { get; set; }
        protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = SaltyDog.CognitoForms.Droid.Resource.Layout.Tabbar;
			ToolbarResource = SaltyDog.CognitoForms.Droid.Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
            ClientHttpConfig = new AmazonCognitoIdentityProviderConfig
            {
                HttpClientFactory = new AndroidClientFactory()

            };
            ClientHttpConfig.RegionEndpoint = RegionEndpoint.USEast1; //set your Endpoint


            
            LoadApplication(new SaltyDog.CognitoForms.App.App(ClientHttpConfig));
		}
	}
}

