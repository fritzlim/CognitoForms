
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaltyDog.CognitoForms;
using SaltyDog.CognitoForms.ViewModels;
using SaltyDog.CognitoForms.Util;

using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public class UpdatePasswordViewModel : CognitoFormsViewModel
	{
		public ICommand CmdUpdate { get; set; }

		public String Password { get; set; }
		public String Password2 { get; set; }

		public UpdatePassword Page { get; internal set; }

		public UpdatePasswordViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator) : base(sessionStore, authApi, navigator)
		{
			CmdUpdate = new Command(DoVerify);
		}

		protected virtual async void DoVerify()
		{
			await Task.Run(async () =>
			{
				try
				{
					var user = SessionStore.UserName.Trim().ToLower();
					var pass = Password.Trim();

					CognitoAction = true;
					var result = await AuthApi.UpdatePassword(user, pass, SessionStore.SessionId);
					CognitoAction = false;

					if ( result.Result == CognitoResult.Ok)
					{
						await PasswordUpdated();
					}
					else
					{
						await PasswordUpdateFailed();
					}
				}
				catch( Exception e)
				{
					Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
				}
			});
		}

		protected virtual async Task PasswordUpdated()
		{
			await Navigator.OnResult(CognitoEvent.PasswordUpdated, this);
		}

		protected virtual async Task PasswordUpdateFailed()
		{
			await Navigator.OnResult(CognitoEvent.PasswordUpdateFailed, this);
		}
	}
}
