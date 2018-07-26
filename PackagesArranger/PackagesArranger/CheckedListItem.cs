using System.ComponentModel;
using PackagesArranger.Model;

namespace PackagesArranger
{
	public class CheckedListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool _isChecked;
		private Package _item;

		public CheckedListItem()
		{
			_item = new Package();
			_isChecked = false;
		}

		public CheckedListItem(Package item, bool isChecked = false)
		{
			_item = item;
			_isChecked = isChecked;
		}

		public Package Item
		{
			get => _item;
			set
			{
				_item = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
			}
		}

		public bool IsChecked
		{
			get => _isChecked;
			set
			{
				_isChecked = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
			}
		}
	}
}