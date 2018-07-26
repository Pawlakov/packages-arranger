using System;
using PackagesArranger.Model;

namespace PackagesArranger.ViewModel
{
	public class ViewModelPackageEditForm : ViewModelWindow
	{
		private readonly CheckedListItem _subject;
		private string _nameField;
		private string _firstDimensionField;
		private string _secondDimensionField;

		public ViewModelPackageEditForm(CheckedListItem subject)
		{
			_subject = subject;
			_nameField = subject.Item.Name;
			_firstDimensionField = subject.Item.FirstDimension.ToString();
			_secondDimensionField = subject.Item.SecondDimension.ToString();
			SubmitChangesCommand = new RelayCommand(SubmitChanges, ValidateValues);
			CancelCommand = new RelayCommand(Cancel);
		}

		public string NameField
		{
			get => _nameField;
			set
			{
				if (_nameField.Equals(value, StringComparison.Ordinal))
					return;
				_nameField = value;
				OnPropertyChanged(nameof(NameField));
			}
		}

		public string FirstDimensionField
		{
			get => _firstDimensionField;
			set
			{
				if (_firstDimensionField.Equals(value, StringComparison.Ordinal))
					return;
				_firstDimensionField = value;
				OnPropertyChanged(nameof(FirstDimensionField));
			}
		}

		public string SecondDimensionField
		{
			get => _secondDimensionField;
			set
			{
				if (_secondDimensionField.Equals(value, StringComparison.Ordinal))
					return;
				_secondDimensionField = value;
				OnPropertyChanged(nameof(SecondDimensionField));
			}
		}

		public string NameText => $"Name (up to {Package.MaxNameLength} chars): ";

		public string FirstDimensionText => $"First dimension ({Package.MinDimension}-{Package.MaxDimension}): ";

		public string SecondDimensionText => $"Second dimension ({Package.MinDimension}-{Package.MaxDimension}): ";

		public RelayCommand SubmitChangesCommand { get; }

		public RelayCommand CancelCommand { get; }

		private void SubmitChanges(object parametr)
		{
			var newPackage = new Package()
			{
			Name = _nameField,
			FirstDimension = Convert.ToInt32(_firstDimensionField),
			SecondDimension = Convert.ToInt32(_secondDimensionField)
			};
			_subject.Item = newPackage;
			OnCloseWindow();
		}

		private void Cancel(object parameter)
		{
			OnCloseWindow();
		}

		private bool ValidateValues(object parameter)
		{
			return Package.ValidateValues(NameField, FirstDimensionField, SecondDimensionField);
		}
	}
}