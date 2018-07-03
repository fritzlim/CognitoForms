using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyDog.CognitoForms.Util
{
	public class DefaultStrings : ICognitoStrings
	{
		public string BadPassTitle { get; set; } = "Bad User id or Password";
		public string BadPassMessage { get; set; } = "There was something wrong with either the user id or password.";
		public string UserNotFoundTitle { get; set; } = "User Not Found";
		public string UserNotFoundMessage { get; set; } = "Could not find a user with that user id";
		public string BadCodeTitle { get; set; } = "Bad Code";
		public string BadCodeMessage { get; set; } = "The validation code was not correct.";
		public string PassUpdateFailedTitle { get; set; } = "Update Password Failed";
		public string PassUpdateFailedMessage { get; set; } = "Could not update the password";
		public string OkButton { get; set; } = "OK";
	}
}
