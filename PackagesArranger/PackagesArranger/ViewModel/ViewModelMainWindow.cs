using System;
using System.Collections.ObjectModel;
using System.Linq;
using PackagesArranger.Model;
using PackagesArranger.View;

namespace PackagesArranger.ViewModel
{
	public class ViewModelMainWindow : ViewModelWindow
	{
		private object _selectedItem;
		private string _containerText;

		public ViewModelMainWindow()
		{
			Container = new Container();
			ContainerText = ContainerText = Container.ToString();
			Container.PropertyChanged += (sender, e) => ContainerText = Container.ToString();
			AllPackages = new ObservableCollection<CheckedListItem>();
			AddCommand = new RelayCommand(Add);
			RemoveCommand = new RelayCommand(Remove, AnythingSelected);
			RemoveAllCommand = new RelayCommand(RemoveAll);
			EditPackageCommand = new RelayCommand(EditPackage, AnythingSelected);
			SelectAllCommand = new RelayCommand(SelectAll);
			UnselectAllCommand = new RelayCommand(UnselectAll);
			EditContainerCommand = new RelayCommand(EditContainer);
			PackCommand = new RelayCommand(Pack);
		}

		public ObservableCollection<CheckedListItem> AllPackages { get; set; }

		public Container Container { get; set; }

		public object SelectedItem
		{
			get => _selectedItem;
			set
			{
				if (value == _selectedItem)
					return;
				_selectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
			}
		}

		public string ContainerText
		{
			get => _containerText;
			set
			{
				if (string.Equals(_containerText, value, StringComparison.Ordinal))
					return;
				_containerText = value;
				OnPropertyChanged(nameof(ContainerText));
			}
		}

		private void Add(object parameter)
		{
			AllPackages.Add(new CheckedListItem());
		}

		private void Remove(object parameter)
		{
			AllPackages.Remove(_selectedItem as CheckedListItem);
		}

		private void RemoveAll(object parameter)
		{
			AllPackages.Clear();
		}

		private void EditPackage(object parameter)
		{
			var window = new PackageEditForm(SelectedItem as CheckedListItem);
			window.ShowDialog();
		}

		private bool AnythingSelected(object parameter)
		{
			return _selectedItem != null;
		}

		private void SelectAll(object parameter)
		{
			foreach (var item in AllPackages)
				item.IsChecked = true;
		}

		private void UnselectAll(object parameter)
		{
			foreach (var item in AllPackages)
				item.IsChecked = false;
		}

		private void EditContainer(object parameter)
		{
			var window = new ContainerEditForm(Container);
			window.ShowDialog();
		}

		private void Pack(object parameter)
		{
			var selectedPackages = (from item in AllPackages where item.IsChecked select item.Item).ToArray();
			var arrangement = Arrangement.ApproximateAlogrithm(Container, selectedPackages);
			if (arrangement == null)
				System.Windows.MessageBox.Show("Not enough space in the container.");
			else
			{
				var window = new ResultWindow(arrangement);
				window.ShowDialog();
			}
		}

		public RelayCommand AddCommand { get; }
		public RelayCommand RemoveCommand { get; }
		public RelayCommand RemoveAllCommand { get; }
		public RelayCommand EditPackageCommand { get; }
		public RelayCommand SelectAllCommand { get; }
		public RelayCommand UnselectAllCommand { get; }
		public RelayCommand EditContainerCommand { get; }
		public RelayCommand PackCommand { get; }
	}
}