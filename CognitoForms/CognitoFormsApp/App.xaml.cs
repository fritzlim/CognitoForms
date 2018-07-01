using System;
using System.Threading.Tasks;
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
			ApiCognito.ClientId = "1sqm1euqob2uretl0jrc961gf3"; // Change to <Your Client Id>
			ApiCognito.RegionEndpoint = RegionEndpoint.USWest2; // Change to <Your Region>

			InitializeMainPage();
		}

		protected void InitializeMainPage()
		{
			// Create a default navigator
			var navigator = new DefaultNavigator
			{
				Authenticated = Authenticated
			};



			// use the default navigator to create and bind the signin page
			PageModelPair pair = navigator.CreatePageModelPair(PageId.SignIn, new ApiCognito(), SessionStore.Instance);

			// Create a navigation page with the signin page
			var navPage = new NavigationPage(pair.Page);

			navigator.Page = pair.Page;
			navigator.Navigation = navPage.Navigation;

			MainPage = navPage;

			MainPage.Title = "Cognito Forms";

		}

		protected async Task Authenticated()
		{
			Device.BeginInvokeOnMainThread( () =>
			{
				MainPage = new NavigationPage(new MainPage());
			});
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
