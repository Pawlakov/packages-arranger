using System;
using PackagesArranger.Model;

namespace PackagesArranger.ViewModel
{
	public class ViewModelContainerEditForm : ViewModelWindow
	{
		private readonly Container _subject;
		private string _lengthField;
		private string _widthField;

		public ViewModelContainerEditForm(Container subject)
		{
			_subject = subject;
			_lengthField = subject.Length.ToString();
			_widthField = subject.Width.ToString();
			SubmitChangesCommand = new RelayCommand(SubmitChanges, ValidateValues);
			CancelCommand = new RelayCommand(Cancel);
		}

		public string LengthField
		{
			get => _lengthField;
			set
			{
				if (_lengthField.Equals(value, StringComparison.Ordinal))
					return;
				_lengthField = value;
				OnPropertyChanged(nameof(LengthField));
			}
		}

		public string WidthField
		{
			get => _widthField;
			set
			{
				if (_widthField.Equals(value, StringComparison.Ordinal))
					return;
				_widthField = value;
				OnPropertyChanged(nameof(WidthField));
			}
		}

		public string LengthText => $"Length ({Container.MinLength}-{Container.MaxLength}): ";

		public string WidthText => $"Width ({Container.MinWidth}-{Container.MaxWidth}): ";

		public RelayCommand SubmitChangesCommand { get; }

		public RelayCommand CancelCommand { get; }

		private void SubmitChanges(object parametr)
		{
			_subject.Length = int.Parse(LengthField);
			_subject.Width = int.Parse(WidthField);
			OnCloseWindow();
		}

		private void Cancel(object parameter)
		{
			OnCloseWindow();
		}

		private bool ValidateValues(object parameter)
		{
			return Container.ValidateValues(LengthField, WidthField);
		}
	}
}