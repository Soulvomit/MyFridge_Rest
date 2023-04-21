using MyFridge_Library_MAUI_DataTransfer.DataTransferObject;
using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipyDetailViewModel 
    {
        private readonly CurrentUserService _cUserService;
        private readonly IngredientAmountService _iaService;
        public Color TextColor { get; set; } = Colors.White;
        public RecipyDto Recipy { get; set; }

        public RecipyDetailViewModel(
            CurrentUserService cUserService,
            IngredientAmountService iaService)
        {
            _cUserService = cUserService;
            _iaService = iaService;
        }

        public bool IsMakable(UserAccountDto user)
        {
            return Recipy.Ingredients
                .All(recipyIngredient => user.Ingredients
                    .Any(userIngredient => recipyIngredient.Id == userIngredient.Id &&
                            recipyIngredient.Amount <= userIngredient.Amount));
        }
        public List<UserIngredientDetailViewModel> ConvertIngredientDtos()
        {
            List<UserIngredientDetailViewModel> viewModels = new();
            foreach (IngredientDto dto in Recipy.Ingredients)
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
