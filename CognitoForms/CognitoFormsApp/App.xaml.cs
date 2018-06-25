using System;
using SaltyDog.CognitoForms.App;
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
