using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SaltyDog.CognitoForms.ViewModels;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms.Util
{

	public class DefaultCognitoFormsNavigator : ICognitoFormsNavigator
	{
		public IApiCognito AuthApi { get; set; }
		public ISessionStore SessionStore { get; set; }
		public INavigation Navigation { get; set; }
		public Page Page { get; set; }

		public virtual async Task OnResult(CognitoEvent ce, CognitoFormsViewModel prior)
		{
			switch (ce)
			{
				case CognitoEvent.Authenticated:
					break;
				case CognitoEvent.BadUserOrPass:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert("Bad User id or Password", "There was something wrong with either the user id or password.", "OK");
					});
					break;
				case CognitoEvent.UserNotFound:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert("User Not Found", "Could not find a user with that user id", "OK");
					});
					break;
				case CognitoEvent.PasswordChangedRequired:
					var pair = CreatePageModelPair(PageId.UpdatePassword, AuthApi, SessionStore);
					pair.Page.BindingContext = pair.ViewModel;

					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PushAsync(pair.Page, true);
					});
					break;
				case CognitoEvent.RegistrationComplete:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopAsync();
					});
					break;
				case CognitoEvent.AccountVerified:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopAsync();
					});
					break;
				case CognitoEvent.PasswordUpdated:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopAsync(true);
					});
					break;
				case CognitoEvent.BadCode:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert("Bad Code", "The validation code was not correct.", "OK");
					});
					break;
				case CognitoEvent.PasswordUpdateFailed:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert("Update Password Failed", "Could not update the password", "OK");
					});
					break;
				default:
					break;
			}
		}

		public virtual PageModelPair CreatePageModelPair(PageId pageId, IApiCognito authApi, ISessionStore sessionStore)
		{
			switch (pageId)
			{
				case PageId.SignIn:
					return new PageModelPair(new SignIn(), new SignInViewModel(sessionStore, authApi, this));
				case PageId.SignUp:
					return new PageModelPair(new SignUp(), new SignUpViewModel(sessionStore, authApi, this));
				case PageId.UpdatePassword:
					return new PageModelPair (new UpdatePassword(), new UpdatePasswordViewModel(sessionStore, authApi, this));
				case PageId.ValidateCode:
					return new PageModelPair(new ValidateCode(), new ValidateCodeViewModel(sessionStore, authApi, this));
				default:
					return null;
			}
		}
	}
}
