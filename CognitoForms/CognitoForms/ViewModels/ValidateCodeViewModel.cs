
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaltyDog.CognitoForms.Util;
using SaltyDog.CognitoForms.ViewModels;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public class ValidateCodeViewModel : CognitoFormsViewModel
	{
		public ICommand CmdValidate { get; set; }
		public ContentPage Page { get; set; }

		public String Email { get; set; }
		public String Code { get; set; }

		public ValidateCodeViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator) : base(sessionStore, authApi, navigator)
		{

			CmdValidate = new Command(DoValidate);

			Email = SessionStore.UserName;
		}

		protected virtual async void DoValidate()
		{
			await Task.Run(async () =>
			{

				try
				{
					var user = Email.Trim().ToLower();
					var pass = Code.Trim();

					CognitoAction = true;
					var result = await AuthApi.VerifyWithCode(user, pass);
					CognitoAction = false;

					if ( result.Result == CognitoResult.Ok)
					{
						await AccountVerified();
					}
					else
					{
						await BadCode();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine($"Exception in {this.GetType().Name} {e.GetType().Name}:{e.Message}");
				}
			});
		}

		protected virtual async Task AccountVerified()
		{
			await Navigator.OnResult(CognitoEvent.AccountVerified, this);
		}

		protected virtual async Task BadCode()
		{
			await Navigator.OnResult(CognitoEvent.BadCode, this);
		}
	}
}
