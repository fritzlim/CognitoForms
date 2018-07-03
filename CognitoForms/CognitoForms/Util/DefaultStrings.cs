using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyDog.CognitoForms.Util
{
	public class DefaultStrings : ICognitoStrings
	{
		public string SignInTitle { get; set; } = "Sign In";
		public string BadPassMessage { get; set; } = "There was something wrong with either the user id or password.";
		public string RequiresValidation { get; set; } = "This account requires validation. Please enter a code.";
		public string UserNotFoundTitle { get; set; } = "User Not Found";
		public string UserNotFoundMessage { get; set; } = "Could not find a user with that user id";
		public string BadCodeTitle { get; set; } = "Bad Code";
		public string BadCodeMessage { get; set; } = "The validation code was not correct.";
		public string PassUpdateFailedTitle { get; set; } = "Update Password Failed";
		public string PassUpdateFailedMessage { get; set; } = "Could not update the password";
		public string SignupFailedTitle { get; set; } = "Signup Error";
		public string UserNameUsed { get; set; } = "The username has already been used";
		public string MinimalPasswordRequirements { get; set; } = "The password does not meet the minimal requirements";
		public string OkButton { get; set; } = "OK";
	}
}
