using System;
using Amazon;
using SaltyDog.CognitoForms.App;
using SaltyDog.CognitoForms.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SaltyDog.CognitoForms.App
{
	public partial class App : Application
	{
		public SessionStore Session { get; set; }

		public App ()
		{
			InitializeComponent();

			ApiCognito.PoolId = "us-west-2_CHjCveWGb"; // Change to <Your Pool Id>
			ApiCognito.ClientId = "1sqm1euqob2uretl0jrc961gf3"; // Change to <Your Client Id>";
			ApiCognito.RegionEndpoint = RegionEndpoint.USWest2;

			var navigator = new DefaultCognitoFormsNavigator();

			PageModelPair pair = navigator.CreatePageModelPair(PageId.SignIn, new ApiCognito(), new SessionStore());
			pair.Page.BindingContext = pair.ViewModel;
			var navPage = new NavigationPage(pair.Page);

			navigator.Page = pair.Page;
			navigator.Navigation = navPage.Navigation;

			MainPage = navPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
