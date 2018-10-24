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
		public IApiCognito AuthApi { get; set; }

		public App ()
		{
			InitializeComponent();

			AuthApi = new ApiCognito();
			Session = new SessionStore { Settings = Plugin.Settings.CrossSettings.Current };

			ApiCognito.PoolId = "us-west-2_CHjCveWGb"; // Change to <Your Pool Id>
			ApiCognito.ClientId = "1sqm1euqob2uretl0jrc961gf3"; // Change to <Your Client Id>
			ApiCognito.RegionEndpoint = RegionEndpoint.USWest2; // Change to <Your Region>

			InitializeMainPage();
		}

       public async void InitializeMainPage()
        {            
            if (Session.IsLoggedIn(DateTime.Now.ToUniversalTime()))
                MainPage = new MainPage(Session);
            else
                Unauthenticated();
        }

		protected void Unauthenticated()
		{
			// Create a default navigator
			var navigator = new DefaultNavigator
			{
				Authenticated = Authenticated,
				AuthApi = AuthApi,
				SessionStore = Session
			};

			// use the default navigator to create and bind the signin page
			PageModelPair pair = navigator.CreatePageModelPair(PageId.SignIn, AuthApi, Session);

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
				MainPage = new NavigationPage(new MainPage(Session));
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
