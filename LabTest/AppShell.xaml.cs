using LabTest.Views;

namespace LabTest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(QuestionOne), typeof(QuestionOne));
            Routing.RegisterRoute(nameof(QuestionThree), typeof(QuestionThree));
        }
    }
}
