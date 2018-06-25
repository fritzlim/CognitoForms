using System;
using System.Threading.Tasks;
using SaltyDog.CognitoForms.ViewModels;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms.Util
{

	public class PageModelPair
	{
		public PageModelPair() { }
		public PageModelPair(ContentPage page, CognitoFormsViewModel viewModel)
		{
			Page = page;
			ViewModel = viewModel;
		}
		public ContentPage Page { get; set; }
		public CognitoFormsViewModel ViewModel { get; set; }
	}

	public enum CognitoEvent
	{
		DoSignin,
		DoSignup,
		Authenticated,
		BadUserOrPass,
		UserNotFound,
		RegistrationComplete,
		AccountVerified,
		BadCode,
		PasswordChangedRequired,
		PasswordUpdated,
		PasswordUpdateFailed
	}


	public interface ICognitoFormsNavigator
	{
		PageModelPair CreatePageModelPair(PageId pageId, IApiCognito authApi, ISessionStore sessionStore);
		Task OnResult(CognitoEvent ce, CognitoFormsViewModel prior);
	}
}