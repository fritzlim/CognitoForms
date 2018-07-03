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

		private bool _isBusy = false;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				Device.BeginInvokeOnMainThread(() =>
				{
					NotifyPropertyChanged(nameof(IsNotBusy));
					NotifyPropertyChanged(nameof(IsBusy));
				});
			}
		}

		/// <summary>
		/// Convenience for binding
		/// </summary>
		public bool IsNotBusy
		{
			get { return !_isBusy; }
		}

		public CognitoFormsViewModel(ISessionStore sessionStore, IApiCognito authApi, ICognitoFormsNavigator navigator)
		{
			SessionStore = sessionStore;
			AuthApi = authApi;
			Navigator = navigator;
		}

	}
}
