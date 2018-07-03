
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

		public String UserName;
		public String Password;
		public String Password2;

		public SessionStore SessionStore { get; set; }
		public IApiCognito ApiAuth { get; set; }


		public SignUpViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator) : base(sessionStore, authApi, navigator)
		{
			SessionStore = SessionStore.Instance;
			ApiAuth = ApiAuth;

			CmdSignUp = new Command(DoSignUp);
		}

		protected virtual async void DoSignUp()
		{
			await Task.Run(async () =>
			{
				try
				{
					var user = UserName?.Trim().ToLower();
					var pass = Password.Trim();

					CognitoAction = true;
					var result = await ApiAuth.SignUp(user, pass);
					CognitoAction = false;

					SessionStore.UserName = user;

					await OnRegistrationComplete();
				}
				catch (Exception e)
				{
					Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
				}
			});
		}

		protected virtual async Task OnRegistrationComplete()
		{
			await Navigator.OnResult(CognitoEvent.RegistrationComplete, this);
		}

	}
}
