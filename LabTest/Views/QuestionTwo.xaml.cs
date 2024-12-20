using LabTest.ViewModels;

namespace LabTest.Views;

public partial class QuestionTwo : ContentPage
{
	public QuestionTwo(QuestionTwoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}