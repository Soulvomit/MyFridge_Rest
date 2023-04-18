using MyFridge_UserInterface_MAUI.Service;

namespace MyFridge_UserInterface_MAUI.ViewModel
{
    public class RecipyViewModel
    {
        public RecipyService RecipyService { get; private set; }
        public IngredientAmountService IAService { get; private set; }
        public CurrentUserService CUserService { get; private set; }
        public List<RecipyDetailViewModel> All { get; set; }
        public List<RecipyDetailViewModel> Makeable { get; set; }

        public RecipyViewModel(
            RecipyService recipyService, 
            CurrentUserService userService, 
            IngredientAmountService iaService)
        {
            RecipyService = recipyService;
            CUserService = userService;
            All = new List<RecipyDetailViewModel>();
            Makeable = new List<RecipyDetailViewModel>();
        }

    }
}
