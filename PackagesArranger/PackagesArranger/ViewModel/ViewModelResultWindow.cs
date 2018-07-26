using System.Collections.ObjectModel;
using PackagesArranger.Model;

namespace PackagesArranger.ViewModel
{
	public class ViewModelResultWindow : ViewModelWindow
	{
		private readonly Arrangement _arrangement;

		public ViewModelResultWindow(Arrangement arrangement)
		{
			_arrangement = arrangement;
			Placements = new ObservableCollection<Placement>(_arrangement.Placements);
		}

		public ObservableCollection<Placement> Placements { get; }

		public double ViewWidth
		{
			get => _arrangement.Container.Width * 0.0685;
		}

		public double ViewLength
		{
			get => _arrangement.Container.Length * 0.0685;
		}
	}
}