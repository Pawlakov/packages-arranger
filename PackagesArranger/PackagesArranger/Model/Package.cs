using System;
using System.ComponentModel;

namespace PackagesArranger.Model
{
	public class Package : INotifyPropertyChanged
	{
		private int _firstDimension = DefaultLength;
		private int _secondDimension = DefaultWidth;
		private string _name = DefaultName;

		public int FirstDimension
		{
			get => _firstDimension;
			set
			{
				if (_firstDimension == value)
					return;
				if (value < MinDimension || value > MaxDimension)
					throw new Exception("Package dimension out of range.");
				_firstDimension = value;
				OnPropertyChanged(nameof(FirstDimension));
			}
		}

		public int SecondDimension
		{
			get => _secondDimension;
			set
			{
				if (_secondDimension == value)
					return;
				if (value < MinDimension || value > MaxDimension)
					throw new Exception("Package dimension out of range.");
				_secondDimension = value;
				OnPropertyChanged(nameof(SecondDimension));
			}
		}

		public string Name
		{
			get => _name;
			set
			{
				if (_name.Equals(value, StringComparison.Ordinal))
					return;
				if (value.Length == 0 || value.Length > MaxNameLength)
					throw new Exception("Package name length out of range.");
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public int Surface => FirstDimension * SecondDimension;

		public event PropertyChangedEventHandler PropertyChanged;

		public override string ToString()
		{
			return $"{Name}({FirstDimension}x{SecondDimension}mm)";
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public static int MinDimension { get; } = 400;
		public static int MaxDimension { get; } = 4000;
		public static int MaxNameLength { get; } = 16;
		public static int DefaultLength { get; } = 1200;
		public static int DefaultWidth { get; } = 800;
		public static string DefaultName { get; } = "Package";

		public static bool ValidateValues(string nameField, string firstDimensionField, string secondDimensionField)
		{
			if (nameField.Length > MaxNameLength ||
			    nameField.Length == 0 ||
			    !int.TryParse(firstDimensionField, out _) ||
			    !int.TryParse(secondDimensionField, out _))
				return false;
			var first = int.Parse(firstDimensionField);
			var second = int.Parse(secondDimensionField);
			return first >= MinDimension && first <= MaxDimension && second >= MinDimension && second <= MaxDimension;
		}
	}
}