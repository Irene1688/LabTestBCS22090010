using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LabTest.ViewModels
{
    public partial class QuestionTwoViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanRegister))]
        [NotifyPropertyChangedFor(nameof(IsPhoneInvalid))]
        private string phoneNumber;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanRegister))]
        [NotifyPropertyChangedFor(nameof(IsPasswordInvalid))]
        private string password;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanRegister))]
        private bool isTermsAccepted;

        public bool IsPhoneInvalid => !string.IsNullOrEmpty(PhoneNumber) && !IsValidPhone(PhoneNumber);
        public bool IsPasswordInvalid => !string.IsNullOrEmpty(Password) && Password.Length <= 5;
        public bool CanRegister => !IsPhoneInvalid && !IsPasswordInvalid && IsTermsAccepted;

        public ICommand RegisterCommand { get; }

        public QuestionTwoViewModel()
        {
            RegisterCommand = new RelayCommand(Register, () => CanRegister);
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CanRegister))
                {
                    (RegisterCommand as RelayCommand)?.NotifyCanExecuteChanged();
                }
            };
        }


        private void Register()
        {
        }

        private static bool IsValidPhone(string phone)
        {
            return !string.IsNullOrEmpty(phone) && System.Text.RegularExpressions.Regex.IsMatch(phone, @"^0[0-9]{9,10}$");
        }
    }

}