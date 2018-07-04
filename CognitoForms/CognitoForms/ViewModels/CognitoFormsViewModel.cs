using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SaltyDog.CognitoForms.Util;
using Xamarin.Forms;

namespace SaltyDog.CognitoForms.ViewModels
{
	/// <summary>
	/// Base class for Coginto Forms ViewModels. 
	/// </summary>
	public class CognitoFormsViewModel : INotifyPropertyChanged
	{

		#region Property Changed Stuff
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Convenience method. Sends the property changed notification for name
		/// </summary>
		/// <param name="name">Name of property</param>
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

		/// <summary>
		/// Indicates when the cognito API is busy. Can be bound for activity indicators, etc.
		/// </summary>
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
