using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SaltyDog.CognitoForms.ViewModels;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms.Util
{
	/// <summary>
	/// Returned from a factory method in the default navigator.
	/// </summary>
	public class PageModelPair
	{
		public PageModelPair() { }
		public PageModelPair(ContentPage page, CognitoFormsViewModel viewModel)
		{
			Page = page;
			ViewModel = viewModel;
		}

		/// <summary>
		/// The page
		/// </summary>
		public Xamarin.Forms.ContentPage Page { get; set; }

		/// <summary>
		/// The page's correspoinding view model.
		/// </summary>
		public CognitoFormsViewModel ViewModel { get; set; }
	}

	/// <summary>
	/// A default implementation of ICognitoFormsNavigator. This implementation presumes accounts must be validated via validation codes as opposed to links.
	/// </summary>
	public class DefaultNavigator : ICognitoFormsNavigator
	{
		public IApiCognito AuthApi { get; set; }
		public ISessionStore SessionStore { get; set; }
		public INavigation Navigation { get; set; }
		public Page Page { get; set; }
		public Func<Task> Authenticated { get; set; }

		public static ICognitoStrings _defaultStrings;
		public ICognitoStrings Strings
		{
			get { return _defaultStrings != null ? _defaultStrings : (_defaultStrings = new DefaultStrings()); }
			set { _defaultStrings = value; }
		}


		/// <summary>
		/// Implements a simple navigation and message stateless state machine. The prior parameter is the ViewModel of the 
		/// page that caused the transition. It may be null, so test before using. This is included in case there may be information
		/// relevant to actions or messages. This method may not be called on the UI thread.	
		/// </summary>
		/// <param name="ce">The event to respond to</param>
		/// <param name="prior">The ViewModel of the screen that caused the transition. May be null.</param>
		/// <returns></returns>
		public virtual async Task OnResult(CognitoEvent ce, CognitoFormsViewModel prior)
		{
			PageModelPair pair = null;
			switch (ce)
			{
				case CognitoEvent.DoSignup:
					pair = CreatePageModelPair(PageId.SignUp, AuthApi, SessionStore);
					pair.Page.BindingContext = pair.ViewModel;

					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PushAsync(pair.Page, true);
					});
					break;
				case CognitoEvent.Authenticated:
					await Authenticated?.Invoke();
					break;
				case CognitoEvent.BadPassword:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert(Strings.SignInTitle, Strings.BadPassMessage, Strings.OkButton);
					});
					break;
				case CognitoEvent.UserNotFound:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert(Strings.UserNotFoundTitle, Strings.UserNotFoundMessage, Strings.OkButton);
					});
					break;
				case CognitoEvent.PasswordChangedRequired:
					pair = CreatePageModelPair(PageId.UpdatePassword, AuthApi, SessionStore);
					pair.Page.BindingContext = pair.ViewModel;

					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PushAsync(pair.Page, true);
					});
					break;
				case CognitoEvent.RegistrationComplete:
					Device.BeginInvokeOnMainThread(async () =>
					{
						pair = CreatePageModelPair(PageId.ValidateCode, AuthApi, SessionStore);
						pair.Page.BindingContext = pair.ViewModel;

						await Navigation.PushAsync(pair.Page, true);
					});
					break;
				case CognitoEvent.AccountVerified:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopToRootAsync();
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
						await Page.DisplayAlert(Strings.BadCodeTitle, Strings.BadCodeMessage, Strings.OkButton);
					});
					break;
				case CognitoEvent.PasswordUpdateFailed:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert(Strings.PassUpdateFailedTitle, Strings.PassUpdateFailedMessage, Strings.OkButton);
					});
					break;
				case CognitoEvent.UserNameAlreadyUsed:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert(Strings.SignupFailedTitle, Strings.UserNameUsed, Strings.OkButton);
					});
					break;
				case CognitoEvent.PasswordRequirementsFailed:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert(Strings.SignupFailedTitle, Strings.MinimalPasswordRequirements, Strings.OkButton);
					});
					break;
				case CognitoEvent.AccountConfirmationRequired:
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Page.DisplayAlert(Strings.SignInTitle, Strings.RequiresValidation, Strings.OkButton);

						pair = CreatePageModelPair(PageId.ValidateCode, AuthApi, SessionStore);
						pair.Page.BindingContext = pair.ViewModel;

						await Navigation.PushAsync(pair.Page, true);
					});
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Creates a page and a model depending on what the pageId is. To add new or different page/view model combinations, subclass and override this method. 
		/// </summary>
		/// <param name="pageId"></param>
		/// <param name="authApi"></param>
		/// <param name="sessionStore"></param>
		/// <param name="bindContext"></param>
		/// <returns></returns>
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
