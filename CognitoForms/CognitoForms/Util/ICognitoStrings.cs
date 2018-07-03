using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyDog.CognitoForms.Util
{
	public interface ICognitoStrings
	{
		string SignInTitle { get; set; }
		string BadPassMessage { get; set; }
		string RequiresValidation { get; set; }
		string UserNotFoundTitle { get; set; }
		string UserNotFoundMessage { get; set; }
		string BadCodeTitle { get; set; }
		string BadCodeMessage { get; set; }
		string PassUpdateFailedTitle { get; set; }
		string PassUpdateFailedMessage { get; set; }
		string SignupFailedTitle { get; set; }
		string UserNameUsed { get; set; }
		string MinimalPasswordRequirements { get; set; }

		string OkButton { get; set; }
	}
}
