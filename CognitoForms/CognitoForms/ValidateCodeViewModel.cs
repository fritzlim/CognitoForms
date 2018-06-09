
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public class ValidateCodeViewModel 
	{
		public ICommand CmdValidate { get; set; }
		public ContentPage Page { get; set; }


		public String Email { get; set; }
		public String Code { get; set; }

		public SessionStore SessionStore { get; set; }
		public IApiCognito ApiAuth { get; set; }



		public ValidateCodeViewModel() 
		{

			SessionStore = SessionStore.Instance;
			ApiAuth = new ApiCognito();

			CmdValidate = new Command(DoValidate);

			Email = SessionStore.UserName;
		}

		protected async void DoValidate()
		{
			await Task.Run(async () =>
			{

				try
				{
					var user = Email.Trim().ToLower();
					var pass = Code.Trim();
					var result = await ApiAuth.VerifyWithCode(user, pass);

					if ( result.Result == CognitoResult.Ok)
					{
						await AccountVerified();
					}
					else
					{
						await AccountVerifyFailed();
					}
				}
				finally
				{
					//
				}
			});
		}

		protected async Task AccountVerified()
		{
			// DO something
			await Page.Navigation.PopAsync();
		}

		protected async Task AccountVerifyFailed()
		{
			Console.WriteLine("Account verify failed.");
		}
	}
}
