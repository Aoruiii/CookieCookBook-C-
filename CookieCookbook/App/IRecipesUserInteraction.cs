using CookieCookbook.Recipes;
using CookieCookbook.Recipes.Ingredients;

namespace CookieCookbook.App;

public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void PrintExistingRecipes(IEnumerable<Recipe> recipes);

    void PromptToCreateNewRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUsers();

}

