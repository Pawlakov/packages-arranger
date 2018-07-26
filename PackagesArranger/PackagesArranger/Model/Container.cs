using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PackagesArranger.Model
{
	public class Container : INotifyPropertyChanged
	{
		private int _length = DefaultLength;
		private int _width = DefaultWidth;

		public int Length
		{
			get => _length;
			set
			{
				if (_length == value)
					return;
				if (value < MinLength || value > MaxLength)
					throw new Exception("Container length out of range.");
				_length = value;
				OnPropertyChanged(nameof(Length));
			}
		}

		public int Width
		{
			get => _width;
			set
			{
				if (_width == value)
					return;
				if (value < MinWidth || value > MaxWidth)
					throw new Exception("Container width out of range.");
				_width = value;
				OnPropertyChanged(nameof(Width));
			}
		}

		public int Surface => Length * Width;

		public event PropertyChangedEventHandler PropertyChanged;

		public override string ToString()
		{
			return $"Container: {Length}x{Width}mm";
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public static int MinLength { get; } = 5000;
		public static int MaxLength { get; } = 20000;
		public static int MinWidth { get; } = 1000;
		public static int MaxWidth { get; } = 5000;
		public static int DefaultLength { get; } = 13600;
		public static int DefaultWidth { get; } = 2400;

		public static bool ValidateValues(string lengthField, string widthField)
		{
			if (!int.TryParse(lengthField, out _) || !int.TryParse(widthField, out _))
				return false;
			var length = int.Parse(lengthField);
			var width = int.Parse(widthField);
			return length >= MinLength && length <= MaxLength && width >= MinWidth && width <= MaxWidth;
		}
	}
}