using PackagesArranger.Model;
using PackagesArranger.ViewModel;

namespace PackagesArranger.View
{
	public partial class ContainerEditForm
	{
		public ContainerEditForm(Container subject)
		{
			InitializeComponent();
			var viewModel = new ViewModelContainerEditForm(subject);
			DataContext = viewModel;
			viewModel.CloseWindow += (sender, e) => { Close(); };
		}
	}
}
