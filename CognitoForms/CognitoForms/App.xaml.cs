using System;
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

			var signIn = new SignIn();
			var signInModel = new SignInViewModel();
			signIn.BindingContext = signInModel;
			signInModel.Page = signIn;

			MainPage = signIn;
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
