using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service.UoW.Interface;
using MyFridge_UserInterface_MAUI.ViewModel.Detail;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class UserViewModel : BindableObject
    {
        private readonly IUnitOfWork _uow;
        public string FirstName
        {
            get => _uow.UserClient.Lazy.FirstName;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                _uow.UserClient.Lazy.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _uow.UserClient.Lazy.LastName;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                _uow.UserClient.Lazy.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => _uow.UserClient.Lazy.Email;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                _uow.UserClient.Lazy.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _uow.UserClient.Lazy.Password;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                _uow.UserClient.Lazy.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string PhoneNumber
        {
            get => _uow.UserClient.Lazy.PhoneNumber.ToString();
            set
            {
                if (!ulong.TryParse(value, out ulong phoneNumber)) return;

                _uow.UserClient.Lazy.PhoneNumber = phoneNumber;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public DateTime BirthDate
        {
            get => _uow.UserClient.Lazy.BirthDate;
            set
            {
                _uow.UserClient.Lazy.BirthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public UserViewModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task SaveAsync()
        {
            if (string.IsNullOrEmpty(FirstName)) return;
            if (string.IsNullOrEmpty(LastName)) return;
            if (string.IsNullOrEmpty(Email)) return;
            if (string.IsNullOrEmpty(Password)) return;
            if (!ulong.TryParse(PhoneNumber.ToString(), out ulong phoneNumber)) return;

            await _uow.UserClient.UpsertAsync(_uow.UserClient.Lazy);
        }

        public ObservableCollection<DetailIngredientViewModel> IngredientsToViewModel()
        {
            ObservableCollection<DetailIngredientViewModel> viewModels = new();
            foreach (IngredientAmountDto dto in _uow.UserClient.Lazy.Ingredients)
            {
                DetailIngredientViewModel viewModel = new(_uow)
                {
                    IngredientAmount = dto
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
