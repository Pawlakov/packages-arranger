using PackagesArranger.ViewModel;

namespace PackagesArranger.View
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			var viewModel = new ViewModelMainWindow();
			DataContext = viewModel;
			viewModel.CloseWindow += (sender, e) => { Close(); };
		}
	}
}
