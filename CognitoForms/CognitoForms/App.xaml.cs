using System;
using Amazon;
using SaltyDog.CognitoForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CognitoForms
{
	public partial class App : Application
	{
		public SessionStore Session { get; set; }

		public App ()
		{
			InitializeComponent();

			ApiCognito.PoolId = "us-west-2_CHjCveWGb"; // "ap-southeast-1_QXqWhPGaK";
			ApiCognito.ClientId = "1sqm1euqob2uretl0jrc961gf3"; // "4vmfhpckenlmepj9vs12kmfru7";
			ApiCognito.RegionEndpoint = RegionEndpoint.USWest2;

			var signIn = new SignIn();
			var signInModel = new SignInViewModel();
			signIn.BindingContext = signInModel;
			signInModel.Page = signIn;

			MainPage = new NavigationPage(signIn);
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
