using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.ComponentModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly CurrentUserService _cUserService;
        private readonly IngredientAmountService _iaService;

        public UserAccountDto User { get; private set; }
        public string Firstname
        {
            get => User.Firstname;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                User.Firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }
        public string Lastname
        {
            get => User.Lastname;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                User.Lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public string Email
        {
            get => User.Email;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                User.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => User.Password;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                User.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ulong PhoneNumber
        {
            get => User.PhoneNumber;
            set
            {
                if (!ulong.TryParse(User.PhoneNumber.ToString(), out ulong phonenum)) return;

                User.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public DateTime BirthDate
        {
            get => User.BirthDate;
            set
            {
                User.BirthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public UserViewModel(CurrentUserService cUserService, IngredientAmountService iaService)
        {
            _cUserService = cUserService;          
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Initialize(int userId)
        {
            _cUserService.CurrentUserId = userId;
            User = await _cUserService.GetUserAsync();
        }

        public async Task Reinitialize()
        {
            UserAccountDto user = await _cUserService.GetUserLazyAsync();
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Password = user.Password;
            PhoneNumber = user.PhoneNumber;
            BirthDate = user.BirthDate;
            User = user;
        }

        public async Task Commit()
        {
            if (string.IsNullOrEmpty(User.Firstname)) return;
            if (string.IsNullOrEmpty(User.Lastname)) return;
            if (string.IsNullOrEmpty(User.Email)) return;
            if (string.IsNullOrEmpty(User.Password)) return;
            if (!ulong.TryParse(User.PhoneNumber.ToString(), out ulong phonenum)) return;

            await _cUserService.UserClient.UpsertAsync(User);
        }

        public List<UserIngredientDetailViewModel> ConvertIngredientDtos()
        {
            List<UserIngredientDetailViewModel> viewModels = new();
            foreach (IngredientDto dto in User.Ingredients)
            {
                UserIngredientDetailViewModel viewModel = new(_cUserService, _iaService)
                {
                    Ingredient = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
