using LabTest.ViewModels;

namespace LabTest.Views;

public partial class QuestionThree : ContentPage
{
	public QuestionThree(QuestionThreeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}