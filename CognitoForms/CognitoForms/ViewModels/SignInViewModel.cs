﻿
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
		public ICommand CmdSignIn { get; set; }
		public ICommand CmdSignUp { get; set; }
		public ContentPage Page { get; set; }

		public String UserName { get; set; }
		public String Password { get; set; }

		public SignInViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator) : base(sessionStore, authApi, navigator)
		{
			CmdSignIn = new Command(DoSignIn);
			CmdSignUp = new Command(DoSignup);
		}

		protected async void DoSignup()
		{
			var signup = new SignUp();
			await Page.Navigation.PushAsync(signup, true);
		}

		protected async void DoSignIn()
		{
			await Task.Run(async () =>
			{
				try
				{
					var user = UserName?.Trim().ToLower();
					var pass = Password?.Trim();
					CognitoAction = true;
					var result = await AuthApi.SignIn(user, pass);
					CognitoAction = false;

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
				}
				catch (Exception e)
				{
					Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
				}
			});
		}

		private async Task OnPasswordChangeRequired()
		{
			await Navigator.OnResult(CognitoEvent.PasswordChangedRequired, this);
		}

		private async Task OnNotAuthorized()
		{
			await Navigator.OnResult(CognitoEvent.BadUserOrPass, this);
		}

		private async Task OnAuthenticated()
		{
			await Navigator.OnResult(CognitoEvent.Authenticated, this);
		}
	}
}