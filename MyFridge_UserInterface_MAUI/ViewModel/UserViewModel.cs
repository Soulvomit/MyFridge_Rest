using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserViewModel : BindableObject
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

        public async Task InitializeAsync(int userId)
        {
            _cUserService.CurrentUserId = userId;
            User = await _cUserService.GetUserAsync();
        }

        public async Task ReinitializeAsync()
        {
            UserAccountDto user = await _cUserService.GetUserLazyAsync();
            User = user;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Password = user.Password;
            PhoneNumber = user.PhoneNumber;
            BirthDate = user.BirthDate; 
        }

        public async Task SaveAsync()
        {
            if (string.IsNullOrEmpty(User.Firstname)) return;
            if (string.IsNullOrEmpty(User.Lastname)) return;
            if (string.IsNullOrEmpty(User.Email)) return;
            if (string.IsNullOrEmpty(User.Password)) return;
            if (!ulong.TryParse(User.PhoneNumber.ToString(), out ulong phonenum)) return;

            await _cUserService.Client.UpsertAsync(User);
        }

        public ObservableCollection<IngredientAmountDetailViewModel> IngredientsToViewModel()
        {
            ObservableCollection<IngredientAmountDetailViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in User.Ingredients)
            {
                IngredientAmountDetailViewModel viewModel = new(_cUserService, _iaService)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
