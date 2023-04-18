using MyFridge_Library_MAUI_Client;

namespace MyFridge_UserInterface_MAUI.Service
{
    public class IngredientAmountService
    {
        public IngredientAmountApiClient IngredientAmountClient { get; private set; }

        public IngredientAmountService()
        {
            string baseAddr = "https://localhost:44364/";
            IngredientAmountClient = new IngredientAmountApiClient(baseAddr);
        }
    }
}
