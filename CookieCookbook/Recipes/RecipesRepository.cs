using CookieCookbook.Recipes.Ingredients;
using CookieCookbook.DataAccess;

namespace CookieCookbook.Recipes;

public class RecipesRepository : IRecipesRepository
{
    private readonly IIngredientRegister _ingredientRegister;
    private readonly IStringsRepository _stringTextualRepository;

    public RecipesRepository(IStringsRepository stringTextualRepository, IIngredientRegister ingredientRegister)
    {
        _stringTextualRepository = stringTextualRepository;
        _ingredientRegister = ingredientRegister;
    }

    public List<Recipe> Read(string filePath)
    {
        List<string> recipeIdsList = _stringTextualRepository.ReadFromFile(filePath);

        var recipes = new List<Recipe>();

        foreach (string recipeIds in recipeIdsList)
        {
            var recipeInPorcess = new List<Ingredient>();
            var ingredientIdList = recipeIds.Split(",");
            foreach (string idString in ingredientIdList)
            {
                int Id = int.Parse(idString);
                recipeInPorcess.Add(_ingredientRegister.GetById(Id));
            }
            recipes.Add(new Recipe(recipeInPorcess));
        }

        return recipes;
    }


    public void Write(string filePath, IEnumerable<Recipe> allRecipes)
    {
        var fileContent = ConvertRecipesToStrings(allRecipes);
        _stringTextualRepository.WriteToFile(fileContent, filePath);
    }

    private List<string> ConvertRecipesToStrings(IEnumerable<Recipe> allRecipes)
    {
        var result = new List<string>();
        foreach (Recipe recipe in allRecipes)
        {
            string currentRecipe = string.Join(",", recipe.Ingredients.Select(ingredient => ingredient.Id));
            result.Add(currentRecipe);
        }
        return result;
    }
}

