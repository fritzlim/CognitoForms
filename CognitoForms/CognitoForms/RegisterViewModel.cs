
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public class RegisterViewModel
	{
		public ICommand CmdSignUp { get; set; }

		public String UserName;
		public String Password;
		public String Password2;

		public SessionStore SessionStore { get; set; }
		public IApiCognito ApiAuth { get; set; }


		public RegisterViewModel() 
		{
			SessionStore = SessionStore.Instance;
			ApiAuth = new ApiCognito();

			CmdSignUp = new Command(DoSignUp);
		}

		protected async void DoSignUp()
		{
			await Task.Run(async () =>
			{
				//StartNetworkAction();

				try
				{
					var user = UserName?.Trim().ToLower();
					var pass = Password.Trim();
					var result = await ApiAuth.SignUp(user, pass);

					SessionStore.UserName = user;


					OnRegistrationComplete();
				}
				finally
				{
					//StopNetworkAction();
				}
			});
		}

		protected void OnRegistrationComplete()
		{
			// Do something useful
		}

	}
}
