using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyDog.CognitoForms.Util
{
	public interface ICognitoStrings
	{
		string BadPassTitle { get; set; }
		string BadPassMessage { get; set; }
		string UserNotFoundTitle { get; set; }
		string UserNotFoundMessage { get; set; }
		string BadCodeTitle { get; set; }
		string BadCodeMessage { get; set; }
		string PassUpdateFailedTitle { get; set; }
		string PassUpdateFailedMessage { get; set; }
		string OkButton { get; set; }
	}
}
