using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SaltyDog.CognitoForms.ViewModels;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms.Util
{
	public class DefaultNavigator : ICognitoFormsNavigator
	{
		public IApiCognito AuthApi { get; set; }
		public ISessionStore SessionStore { get; set; }
		public INavigation Navigation { get; set; }
		public Page Page { get; set; }
		public Func<Task> Authenticated { get; set; }

		public Page Initialize<NAV, CP>(IApiCognito apiAuth, ISessionStore sessionStore, Func<CP, NAV> factory) where NAV : NavigationPage, new() where CP : ContentPage
		{
			PageModelPair pair = CreatePageModelPair(PageId.SignIn, new ApiCognito(), new SessionStore());

			// Create a navigation page with the signin page
			var navPage = factory(pair.Page as CP);

			Page = pair.Page;
			Navigation = navPage.Navigation;

			return navPage;
		}

		public virtual async Task OnResult(CognitoEvent ce, CognitoFormsViewModel prior)
		{
			switch (ce)
			{
				case CognitoEvent.Authenticated:
					await Authenticated?.Invoke();
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

		public virtual PageModelPair CreatePageModelPair(PageId pageId, IApiCognito authApi, ISessionStore sessionStore, bool bindContext = true)
		{
			PageModelPair pair = null;

			switch (pageId)
			{
				case PageId.SignIn:
					pair = new PageModelPair(new SignIn(), new SignInViewModel(sessionStore, authApi, this));
					break;
				case PageId.SignUp:
					pair = new PageModelPair(new SignUp(), new SignUpViewModel(sessionStore, authApi, this));
					break;
				case PageId.UpdatePassword:
					pair = new PageModelPair (new UpdatePassword(), new UpdatePasswordViewModel(sessionStore, authApi, this));
					break;
				case PageId.ValidateCode:
					pair = new PageModelPair(new ValidateCode(), new ValidateCodeViewModel(sessionStore, authApi, this));
					break;
			}

			if (pair != null && bindContext)
				pair.Page.BindingContext = pair.ViewModel;

			return pair;
		}
	}
}
