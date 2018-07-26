using PackagesArranger.Model;
using PackagesArranger.ViewModel;

namespace PackagesArranger.View
{
	public partial class ResultWindow
	{
		public ResultWindow(Arrangement arrangement)
		{
			InitializeComponent();
			var viewModel = new ViewModelResultWindow(arrangement);
			DataContext = viewModel;
			viewModel.CloseWindow += (sender, e) => { Close(); };
		}
	}
}
