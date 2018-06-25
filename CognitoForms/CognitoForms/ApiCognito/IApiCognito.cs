using System;
using System.Threading.Tasks;

namespace SaltyDog.CognitoForms
{

	public enum CognitoResult
	{
		Unknown,
		Ok,
		PasswordChangeRequred,
		SignupOk,
		NotAuthorized,
		Error
	}

	public class CognitoContext
	{
		public CognitoContext(CognitoResult res = CognitoResult.Unknown)
		{
			Result = res;
		}
		public CognitoResult Result { get; set; }
	}

	public class SignInContext : CognitoContext
	{
		public SignInContext(CognitoResult res = CognitoResult.Unknown) : base(res)
		{
		}
		//public CognitoUser User { get; set; }
		public String IdToken { get; set; }
		public String AccessToken { get; set; }
		public String RefreshToken { get; set; }
		public String SessionId { get; set; }
		public DateTime TokenIssued { get; set; }
		public DateTime Expires { get; set; }
	}

	public class CredentialsContext : CognitoContext
	{
		public CredentialsContext(CognitoResult res = CognitoResult.Unknown) : base(res)
		{
		}
		//public CognitoUser User { get; set; }
		public string AccessKeyId { get; set; }
		public DateTime Expiration { get; set; }
		public string SecretKey { get; set; }
		public string SessionToken { get; set; }
	}


	public interface IApiCognito 
	{
		Task<SignInContext> SignIn(string userName, string password);
		Task<SignInContext> RefreshToken(string userName, string idToken, string accessToken, String refreshToken, DateTime issued, DateTime expires);
		Task<CognitoContext> SignUp(string userName, string password);
		Task<CognitoContext> ForgotPassword(string userName, string code);
		Task<CognitoContext> VerifyWithCode(string userName, string code);
		Task<CognitoContext> UpdatePassword(string userName, string newPassword, string sessionId);
	}
}


