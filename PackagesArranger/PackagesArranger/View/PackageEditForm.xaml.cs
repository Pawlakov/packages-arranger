using PackagesArranger.ViewModel;

namespace PackagesArranger.View
{
	public partial class PackageEditForm
	{
		public PackageEditForm(CheckedListItem subject)
		{
			InitializeComponent();
			var viewModel = new ViewModelPackageEditForm(subject);
			DataContext = viewModel;
			viewModel.CloseWindow += (sender, e) => { Close(); };
		}
	}
}
