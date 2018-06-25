using System;
using Plugin.Settings.Abstractions;

namespace SaltyDog.CognitoForms
{
	public interface ISessionStore
	{
		string AccessToken { get; set; }
		int ExpiresInSeconds { get; set; }
		string IdToken { get; set; }
		string RefreshToken { get; set; }
		string SessionId { get; set; }
		ISettings Settings { get; set; }
		DateTime TokenExpiresServer { get; set; }
		DateTime TokenIssuedServer { get; set; }
		string UserName { get; set; }

		bool IsLoggedIn(DateTime now);
		void Logout();
	}
}