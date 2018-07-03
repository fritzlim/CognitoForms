using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaltyDog.CognitoForms.Util;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms
{
	public partial class MainPage : ContentPage
	{
		ISessionStore Session { get; set; }

		public MainPage(ISessionStore session)
		{
			Session = session;

			InitializeComponent();

			signOut.Clicked += (s,e) =>
			{
				session.Logout();

				(Application.Current as SaltyDog.CognitoForms.App.App).InitializeMainPage();
			};
		}
	}
}
