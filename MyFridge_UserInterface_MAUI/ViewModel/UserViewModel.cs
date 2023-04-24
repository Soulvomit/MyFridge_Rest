using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserViewModel : BindableObject
    {
        private readonly CurrentUserService _currentUserService;
        private readonly IngredientAmountService _ingredientAmountService;

        public UserAccountDto User { get; private set; }
        public string FirstName
        {
            get => User.FirstName;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                User.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => User.LastName;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                User.LastName = value;
                OnPropertyChanged(nameof(LastName));
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
        public string PhoneNumber
        {
            get => User.PhoneNumber.ToString();
            set
            {
                if (!ulong.TryParse(value, out ulong phoneNumber)) return;

                User.PhoneNumber = phoneNumber;
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
            _currentUserService = cUserService;
        }

        public async Task InitializeAsync(int userId)
        {
            _currentUserService.CurrentUserId = userId;
            User = await _currentUserService.GetUserAsync();
        }

        public async Task ReinitializeAsync()
        {
            UserAccountDto user = await _currentUserService.GetUserLazyAsync();
            User = user;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Password = user.Password;
            PhoneNumber = user.PhoneNumber.ToString();
            BirthDate = user.BirthDate; 
        }

        public async Task SaveAsync()
        {
            if (string.IsNullOrEmpty(User.FirstName)) return;
            if (string.IsNullOrEmpty(User.LastName)) return;
            if (string.IsNullOrEmpty(User.Email)) return;
            if (string.IsNullOrEmpty(User.Password)) return;
            if (!ulong.TryParse(User.PhoneNumber.ToString(), out ulong phoneNumber)) return;

            await _currentUserService.Client.UpsertAsync(User);
        }

        public ObservableCollection<IngredientAmountDetailViewModel> IngredientsToViewModel()
        {
            ObservableCollection<IngredientAmountDetailViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in User.Ingredients)
            {
                IngredientAmountDetailViewModel viewModel = new(_currentUserService, _ingredientAmountService)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
