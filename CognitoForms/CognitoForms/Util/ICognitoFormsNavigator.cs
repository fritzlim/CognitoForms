using System;
using System.Threading.Tasks;
using SaltyDog.CognitoForms.ViewModels;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms.Util
{
	public enum CognitoEvent
	{
		/// <summary>
		/// The sign in screen should be shown.
		/// </summary>
		DoSignin,
		/// <summary>
		/// The signup screen should be shown.
		/// </summary>
		DoSignup,
		/// <summary>
		/// The user has successfully authenticated
		/// </summary>
		Authenticated,
		/// <summary>
		/// The password is invalid
		/// </summary>
		BadPassword,
		/// <summary>
		/// The user was not found.
		/// </summary>
		UserNotFound,
		/// <summary>
		/// Registration has been successfully created.
		/// </summary>
		RegistrationComplete,
		/// <summary>
		/// The account has been verified successfully
		/// </summary>
		AccountVerified,
		/// <summary>
		/// The validation code was invalid. 
		/// </summary>
		BadCode,
		/// <summary>
		/// The default password needs to be changed.
		/// </summary>
		PasswordChangedRequired,
		/// <summary>
		/// The default password has been successfully updated
		/// </summary>
		PasswordUpdated,
		/// <summary>
		/// The default password update has failed.
		/// </summary>
		PasswordUpdateFailed,
		/// <summary>
		/// The password change failed the requirements check
		/// </summary>
		PasswordRequirementsFailed,
		/// <summary>
		/// The user name has already been used.
		/// </summary>
		UserNameAlreadyUsed,
		/// <summary>
		/// Account confirmation is required.
		/// </summary>
		AccountConfirmationRequired
	}

	/// <summary>
	/// The callback interface used by the ViewModels to report events
	/// </summary>
	public interface ICognitoFormsNavigator
	{
		/// <summary>
		/// An object that holds various message box/alert strings
		/// </summary>
		ICognitoStrings Strings { get; set; }
		/// <summary>
		/// Called with events. 
		/// </summary>
		/// <param name="ce">The cognito event</param>
		/// <param name="prior">The ViewModel of the screen that caused the event change. May be null.</param>
		/// <returns></returns>
		Task OnResult(CognitoEvent ce, CognitoFormsViewModel prior);
	}
}