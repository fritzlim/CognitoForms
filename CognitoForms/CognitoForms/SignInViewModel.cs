
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public class SignInViewModel 
	{
		public ICommand CmdSignIn { get; set; }
		public ContentPage Page { get; set; }

		public String UserName { get; set; }
		public String Password { get; set; }

		public SessionStore SessionStore { get; set; }
		public IApiCognito ApiAuth { get; set; }


		public SignInViewModel() 
		{
			SessionStore = SessionStore.Instance;
			ApiAuth = new ApiCognito();

			CmdSignIn = new Command(DoSignIn);
		}

		protected async void DoSignIn()
		{
			await Task.Run(async () =>
			{
				//StartNetworkAction();

				try
				{
					var user = UserName?.Trim().ToLower();
					var pass = Password?.Trim();
					var result = await ApiAuth.SignIn(user, pass);

					if (result.Result == CognitoResult.Ok)
					{
						SessionStore.UserName = user;
						SessionStore.AccessToken = result.AccessToken;
						SessionStore.IdToken = result.IdToken;
						SessionStore.RefreshToken = result.RefreshToken;
						SessionStore.SessionId = result.SessionId;
						SessionStore.TokenIssuedServer = result.TokenIssued;
						SessionStore.TokenExpiresServer = result.Expires;

						await OnAuthenticated();
					}
					else if (result.Result == CognitoResult.NotAuthorized)
					{
						await OnNotAuthorized();
					}
					else if (result.Result == CognitoResult.PasswordChangeRequred)
					{
						SessionStore.UserName = user;
						SessionStore.AccessToken = result.AccessToken;
						SessionStore.IdToken = result.IdToken;
						SessionStore.RefreshToken = result.RefreshToken;
						SessionStore.SessionId = result.SessionId;
						SessionStore.TokenIssuedServer = result.TokenIssued;
						SessionStore.TokenExpiresServer = result.Expires;

						await OnPasswordChangeRequired();
					}
					else
						throw new Exception("Unknown AuthAction");
				}
				finally
				{
					//StopNetworkAction();
				}
			});
		}

		private async Task OnPasswordChangeRequired()
		{
			UpdatePassword updatePassword = new UpdatePassword();
			UpdatePasswordViewModel viewModel = new UpdatePasswordViewModel();

			updatePassword.BindingContext = viewModel;
			viewModel.Page = updatePassword;

			Device.BeginInvokeOnMainThread(async () =>
			{
				await Page.Navigation.PushAsync(updatePassword, true);
			});

		}

		private async Task OnNotAuthorized()
		{
			// Do something useful
		}

		private async Task OnAuthenticated()
		{
			MainPage mainPage = new MainPage();

			Device.BeginInvokeOnMainThread(async () =>
			{
			   await Page.Navigation.PushAsync(mainPage, true);
		    });

		}
	}
}
