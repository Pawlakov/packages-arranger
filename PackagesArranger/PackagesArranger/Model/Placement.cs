using System.ComponentModel;

namespace PackagesArranger.Model
{
	public class Placement : INotifyPropertyChanged
	{
		private int _x;
		private int _y;
		private readonly Package _package;
		private bool _swappedDimensions;

		public Placement(Package package)
		{
			_package = package;
		}

		public double ViewX => X * 0.0685;
		public double ViewY => Y * 0.0685;
		public double ViewWidth => Width * 0.0685;
		public double ViewLength => Length * 0.0685;
		public string Name => _package.Name;
		public double TextWidth => ViewLength - 4;

		public int X
		{
			get => _x;
			set
			{
				if (value == _x)
					return;
				_x = value;
				OnPropertyChanged(nameof(ViewX));
			}
		}

		public int Y
		{
			get => _y;
			set
			{
				if (value == _y)
					return;
				_y = value;
				OnPropertyChanged(nameof(ViewY));
			}
		}

		public int Width => _swappedDimensions ? _package.FirstDimension : _package.SecondDimension;

		public int Length => _swappedDimensions ? _package.SecondDimension : _package.FirstDimension;

		public void SwapDimensions()
		{
			_swappedDimensions = !_swappedDimensions;
			OnPropertyChanged(nameof(ViewWidth));
			OnPropertyChanged(nameof(ViewLength));
			OnPropertyChanged(nameof(TextWidth));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}