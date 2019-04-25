using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SaltyDog.CognitoForms.ViewModels;
using Xamarin.Forms;
using SaltyDog.CognitoForms.Util;

namespace SaltyDog.CognitoForms
{
	public class SignInViewModel : CognitoFormsViewModel
	{
		/// <summary>
		/// Command to sign in
		/// </summary>
		public ICommand CmdSignIn { get; set; }

		/// <summary>
		/// Command to show the sign up screen
		/// </summary>
		public ICommand CmdSignUp { get; set; }

		/// <summary>
		/// The corresponding page.
		/// </summary>
		public ContentPage Page { get; set; }

		public String UserName { get; set; }
		public String Password { get; set; }

		public SignInViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator) : base(sessionStore, authApi, navigator)
		{
			CmdSignIn = new Command(DoSignIn);
			CmdSignUp = new Command(DoSignUp);
		}

		protected virtual async void DoSignUp()
		{
			await Navigator.OnResult(CognitoEvent.DoSignup, this);
		}

		protected virtual async void DoSignIn()
		{
			await Task.Run(async () =>
			{
				try
				{
					var user = UserName?.Trim().ToLower();
					var pass = Password?.Trim();
					IsBusy = true;
					var result = await AuthApi.SignIn(user, pass);
					IsBusy = false;

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
					else if (result.Result == CognitoResult.NotConfirmed)
					{
						await OnConfirmationRequired();
					}
					else if (result.Result == CognitoResult.UserNotFound)
					{
						await OnNoSuchUser();
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
				}
				catch (Exception e)
				{
					Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
				}

			});
		}

		/// <summary>
		/// Called when confirmation is required.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnConfirmationRequired()
		{
			await Navigator.OnResult(CognitoEvent.AccountConfirmationRequired, this);
		}

		/// <summary>
		/// Called when the default password needs to be changed.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnPasswordChangeRequired()
		{
			await Navigator.OnResult(CognitoEvent.PasswordChangedRequired, this);
		}

		/// <summary>
		/// Called when there is no user with that name
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnNoSuchUser()
		{
			await Navigator.OnResult(CognitoEvent.UserNotFound, this);
		}

		/// <summary>
		/// Called when the credentials are not authorized.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnNotAuthorized()
		{
			await Navigator.OnResult(CognitoEvent.BadPassword, this);
		}

		/// <summary>
		/// Called when the user has successfully authenticated.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnAuthenticated()
		{
			await Navigator.OnResult(CognitoEvent.Authenticated, this);
		}
	}
}
