using CookieCookbook.Recipes;
using CookieCookbook.Recipes.Ingredients;

namespace CookieCookbook.App;

public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    private readonly IIngredientRegister _ingredientRegister;

    public RecipesConsoleUserInteraction(IIngredientRegister ingredientRegister)
    {
        _ingredientRegister = ingredientRegister;
    }

    public void ShowMessage(string message)
    {
        System.Console.WriteLine(message);
    }


    public void PrintExistingRecipes(IEnumerable<Recipe> recipes)
    {
        if (recipes.Count() > 0)
        {
            System.Console.WriteLine("Existing recipes are:");
            System.Console.WriteLine();

            int counter = 1;
            foreach (Recipe recipe in recipes)
            {
                System.Console.WriteLine($"***** {counter} *****");
                System.Console.WriteLine(recipe);
                System.Console.WriteLine();
                counter++;
            }
        }

    }

    public void PromptToCreateNewRecipe()
    {
        System.Console.WriteLine("Create a new cookie recipe! " +
        "Available ingredients are:");

        foreach (Ingredient ingredient in _ingredientRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    public IEnumerable<Ingredient> ReadIngredientsFromUsers()
    {
        System.Console.WriteLine("Add an ingredient by its ID or type anything else if finished.");

        int Id;
        var ingredients = new List<Ingredient>();

        while (int.TryParse(Console.ReadLine(), out Id))
        {
            var currentIngredient = _ingredientRegister.GetById(Id);
            if (currentIngredient is not null)
            {
                ingredients.Add(currentIngredient);
            }

        }
        return ingredients;
    }
}

