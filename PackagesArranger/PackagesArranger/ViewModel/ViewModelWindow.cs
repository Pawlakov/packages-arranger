using System;
using System.ComponentModel;

namespace PackagesArranger.ViewModel
{
	public class ViewModelWindow : INotifyPropertyChanged
	{
		public event EventHandler CloseWindow;

		protected void OnCloseWindow()
		{
			CloseWindow?.Invoke(this, EventArgs.Empty);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}