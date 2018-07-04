
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaltyDog.CognitoForms.Util;
using SaltyDog.CognitoForms.ViewModels;
using SaltyDog.CognitoForms.Util;

using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public class SignUpViewModel : CognitoFormsViewModel
	{
		public ICommand CmdSignUp { get; set; }

		private string _userName;
		public String UserName
		{
			get => _userName;
			set { _userName = value; NotifyPropertyChanged(nameof(BasicValidityCheck)); }
		}

		private string _password;
		public String Password
		{
			get => _password;
			set { _password = value; NotifyPropertyChanged(nameof(BasicValidityCheck)); }
		}

		private string _password2;
		public String Password2
		{
			get => _password2;
			set { _password2 = value; NotifyPropertyChanged(nameof(BasicValidityCheck)); }
		}

		/// <summary>
		/// Convenience property to enable/disable the signup button.
		/// </summary>
		public bool BasicValidityCheck
		{
			get
			{
				return IsNotBusy && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Password2) && Password == Password2;
			}
		}


		public SignUpViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator) : base(sessionStore, authApi, navigator)
		{
			CmdSignUp = new Command(DoSignUp);
		}

		protected virtual async void DoSignUp()
		{
			await Task.Run(async () =>
			{
				try
				{
					var user = UserName?.Trim().ToLower();
					var pass = Password?.Trim();

					IsBusy = true;
					var result = await AuthApi.SignUp(user, pass);
					IsBusy = false;

					SessionStore.UserName = user;

					if (result.Result == CognitoResult.SignupOk)
						await OnRegistrationComplete();
					else if (result.Result == CognitoResult.PasswordRequirementsFailed)
						await OnPasswordRequirementsFailed();
					else if (result.Result == CognitoResult.UserNameAlreadyUsed)
						await OnUserNameAlreadyUsed();
					else if (result.Result == CognitoResult.NotConfirmed)
						await OnUserNotConfirmed();

					return;
				}
				catch (Exception e)
				{
					Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
				}
			});
		}

		/// <summary>
		/// Called when the password requirements fail.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnPasswordRequirementsFailed()
		{
			await Navigator.OnResult(CognitoEvent.PasswordRequirementsFailed, this);
		}

		/// <summary>
		/// Called when the server indicates the user is not yet confirmed.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnUserNotConfirmed()
		{
			await Navigator.OnResult(CognitoEvent.AccountConfirmationRequired, this);
		}

		/// <summary>
		/// Called when a user with that name has already registered.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnUserNameAlreadyUsed()
		{
			await Navigator.OnResult(CognitoEvent.UserNameAlreadyUsed, this);
		}

		/// <summary>
		/// Called when registration is complete.
		/// </summary>
		/// <returns></returns>
		protected virtual async Task OnRegistrationComplete()
		{
			await Navigator.OnResult(CognitoEvent.RegistrationComplete, this);
		}

	}
}
