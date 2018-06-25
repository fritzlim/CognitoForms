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

		public virtual async Task OnResult(CognitoEvent ce, CognitoFormsViewModel prior)
		{
			switch (ce)
			{
				case CognitoEvent.Authenticated:
					break;
				case CognitoEvent.BadUserOrPass:
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
					break;
				case CognitoEvent.AccountVerified:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopAsync();
					});
					break;
				case CognitoEvent.BadCode:
					break;
				case CognitoEvent.PasswordUpdated:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopAsync(true);
					});
					break;
				case CognitoEvent.PasswordUpdateFailed:
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
