﻿using MyFridge_Library_MAUI_Client.ClientModel;
using MyFridge_UserInterface_MAUI.Mvvms.Service.Client.Interface;
using MyFridge_UserInterface_MAUI.Mvvms.ViewModel.Detail;
using System.Collections.ObjectModel;

namespace MyFridge_UserInterface_MAUI.Mvvms.ViewModel
{
    public class UserIngredientViewModel : BindableObject
    {
        #region Privates
        private readonly IClientService _clientService;
        private ObservableCollection<DetailIngredientViewModel> userIngredientDetails;
        #endregion

        #region Properties
        public ObservableCollection<DetailIngredientViewModel> UserIngredientDetails
        {
            get => userIngredientDetails;
            private set
            {
                userIngredientDetails = value;
                OnPropertyChanged(nameof(UserIngredientDetails));
            }
        }
        #endregion

        public UserIngredientViewModel(IClientService clientService)
        {
            _clientService = clientService;

            userIngredientDetails = new();
        }
        public async Task RefreshAsync()
        {
            UserAccountDto user = await _clientService.UserClient.GetAsync(_clientService.UserClient.Lazy.Id);
            UserIngredientDetails = ToViewModel(user.IngredientAmounts.OrderBy(i => i.Ingredient.Name));
        }

        public void GetFilteredLazy(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                UserIngredientDetails = ToViewModel(_clientService.UserClient.Lazy.IngredientAmounts
                    .OrderBy(i => i.Ingredient.Name));
            else
                UserIngredientDetails = ToViewModel(_clientService.UserClient.Lazy.IngredientAmounts
                    .Where(i => i.Ingredient.Name.ToLower().StartsWith(filter.ToLower()))
                    .OrderBy(i => i.Ingredient.Name));
        }
        private ObservableCollection<DetailIngredientViewModel> ToViewModel(IEnumerable<IngredientAmountDto> ingredients)
        {
            ObservableCollection<DetailIngredientViewModel> viewModels = new();
            foreach (IngredientAmountDto ingredient in ingredients)
            {
                DetailIngredientViewModel viewModel = new(_clientService)
                {
                    IngredientAmount = ingredient
                };
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}