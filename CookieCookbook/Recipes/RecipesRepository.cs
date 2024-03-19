using CookieCookbook.Recipes.Ingredients;
using CookieCookbook.DataAccess;
using System.Xml.XPath;

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


        var recipes = recipeIdsList.Select(recipeIds =>
        {
            var ingredientList = recipeIds.Split(",").Select(id => _ingredientRegister.GetById(int.Parse(id)));
            return new Recipe(ingredientList);
        }
          );

        return recipes.ToList();

    }


    public void Write(string filePath, IEnumerable<Recipe> allRecipes)
    {
        var fileContent = ConvertRecipesToStrings(allRecipes);
        _stringTextualRepository.WriteToFile(fileContent, filePath);
    }

    private List<string> ConvertRecipesToStrings(IEnumerable<Recipe> allRecipes)
    {

        return allRecipes.Select(recipe => string.Join(",", recipe.Ingredients.Select(ingredient => ingredient.Id))).ToList();
        // var result = new List<string>();
        // foreach (Recipe recipe in allRecipes)
        // {
        //     string currentRecipe = string.Join(",", recipe.Ingredients.Select(ingredient => ingredient.Id));
        //     result.Add(currentRecipe);
        // }
        // return result;
    }
}

