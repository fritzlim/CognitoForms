
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaltyDog.CognitoForms;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public class UpdatePasswordViewModel 
	{
		public ICommand CmdUpdate { get; set; }

		public String Password { get; set; }
		public String Password2 { get; set; }

		public SessionStore SessionStore { get; set; }
		public IApiCognito ApiAuth { get; set; }
		public UpdatePassword Page { get; internal set; }

		public UpdatePasswordViewModel(SessionStore sessionStore, IApiCognito apiCognito) 
		{
			SessionStore = sessionStore;
			ApiAuth = apiCognito;

			CmdUpdate = new Command(DoVerify);
		}

		protected async void DoVerify()
		{
			await Task.Run(async () =>
			{
				//AppNavigation.StartNetworkAction();

				try
				{
					var user = SessionStore.UserName.Trim().ToLower();
					var pass = Password.Trim();
					var result = await ApiAuth.UpdatePassword(user, pass, SessionStore.SessionId);

					if ( result.Result == CognitoResult.Ok)
					{
						await PasswordUpdated();
					}
					else
					{
						await PasswordUpdateFailed();
					}
				}
				finally
				{
					//AppNavigation.StopNetworkAction();
				}
			});
		}

		protected async Task PasswordUpdated()
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				await Page.Navigation.PopAsync(true);
			});
		}

		protected async Task PasswordUpdateFailed()
		{
			Console.WriteLine("Update password failed.");
		}
	}
}
