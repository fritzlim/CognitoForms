using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SaltyDog.CognitoForms.Util;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms.ViewModels
{
	public class CognitoFormsViewModel : INotifyPropertyChanged
	{

		#region Property Changed Stuff
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
		#endregion

		#region
		public ISessionStore SessionStore { get; set; }
		public IApiCognito AuthApi { get; set; }
		public ICognitoFormsNavigator Navigator { get; set; }
		#endregion

		private bool _cognitoAction = false;
		public bool CognitoAction
		{
			get { return _cognitoAction; }
			set
			{
				_cognitoAction = value;
				NotifyPropertyChanged(nameof(CognitoAction));
			}
		}


		public CognitoFormsViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator)
		{
			SessionStore = sessionStore;
			AuthApi = authApi;
			Navigator = navigator;
		}

	}
}
