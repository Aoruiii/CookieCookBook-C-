// See https://aka.ms/new-console-template for more information
using System.Net.NetworkInformation;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;

var cookBookApp = new CookBookApp(new RecipesRepository(), new RecipesConsoleUserInteraction());
cookBookApp.Run();

public class CookBookApp
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;


    public CookBookApp(IRecipesRepository recipesRepository, IRecipesUserInteraction recipesUserInteraction)
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }

    public void Run()
    {
        var allRecipes = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);

        _recipesUserInteraction.PromptToCreateNewRecipe();

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUsers();

        if (ingredients.Count > 0)
        {
            var recipe = new Recipe(ingredients);
            allRecipes.Add(recipe);
            _recipesRepository.Write(filePath, allRecipes);
            _recipesUserInteraction.ShowMessage("Recipe added:");
            _recipesUserInteraction.ShowMessage(recipe.ToString());
        }
        else
        {
            _recipesUserInteraction.ShowMessage(
                "No ingredients have been selected." +
                "Recipe will not be saved."
            );
        }

    }
}


public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
}

public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    public void ShowMessage(string message)
    {
        System.Console.WriteLine(message);
    }
}

public interface IRecipesRepository
{

}

public class RecipesRepository : IRecipesRepository
{

}






// var cookBook = new CookBook();
// cookBook.PrintExistingRecipes();
// Console.WriteLine("Create a new cookie recipe! Available ingredients are:");
// cookBook.PrintAvailableIngredients();
// cookBook.CreateNewRecipe();


public class CookBook
{
    public List<Recipe> Recipes { get; } = new();

    public List<Ingredient> allIngredients = new List<Ingredient>
        {
            new WheatFlour(),
            new CoconutFlour(),
            new Butter(),
            new Chocolate(),
            new Sugar(),
            new Cardamom(),
            new Cinnamon(),
            new CocoaPowder(),
        };

    private string _filePath = "recipe.json";
    public void ReadRecipes()
    {
        List<string> recipeIdsList = new TxtFileOperation().ReadFromFile(_filePath);
        // Read recipes to the Recipes Property
        foreach (string recipeIds in recipeIdsList)
        {
            var recipeInPorcess = new List<Ingredient>();
            var ingredientIdList = recipeIds.Split(",");
            foreach (string idString in ingredientIdList)
            {
                int ID = int.Parse(idString);
                recipeInPorcess.Add(allIngredients[ID - 1]);
            }
            Recipes.Add(new Recipe(recipeInPorcess));
        }
    }

    public void PrintAvailableIngredients()
    {
        for (int i = 0; i < allIngredients.Count; i++)
        {
            Console.WriteLine($"{allIngredients[i].ID}. {allIngredients[i].Name}");
        }
    }

    public void PrintExistingRecipes()
    {
        ReadRecipes();
        if (Recipes.Count > 0)
        {
            System.Console.WriteLine("Existing recipes are:");
            System.Console.WriteLine();

            for (int i = 0; i < Recipes.Count; i++)
            {
                System.Console.WriteLine($"***** {i + 1} *****");
                Recipes[i].PrintSingleRecipe();
            }
        }

    }

    public void CreateNewRecipe()
    {
        System.Console.WriteLine("Add an ingredient by its ID or type anything else if finished.");
        int result;
        var userInputNums = new List<int>();
        var useInputIngredients = new List<Ingredient>();

        while (int.TryParse(Console.ReadLine(), out result))
        {
            userInputNums.Add(result);
        }

        foreach (int num in userInputNums)
        {
            foreach (Ingredient ingredient in allIngredients)
            {
                if (num == ingredient.ID)
                {
                    useInputIngredients.Add(ingredient);
                }
            }
        }

        if (useInputIngredients.Count == 0)
        {
            Console.WriteLine("No ingredients have been selected. Recipe will not be saved.");
        }
        else
        {
            var newRecipe = new Recipe(useInputIngredients);
            Console.WriteLine("Recipe added:");
            newRecipe.PrintSingleRecipe();
            Recipes.Add(newRecipe);
            StoreRecipes();
        }

    }

    public void StoreRecipes()
    {
        var fileContent = ConvertRecipesToStrings();
        new TxtFileOperation().StoreToFile(fileContent, _filePath);
    }

    private List<string> ConvertRecipesToStrings()
    {
        var result = new List<string>();
        foreach (Recipe recipe in Recipes)
        {
            string currentRecipe = string.Join(",", recipe.Ingredients.Select(ingredient => ingredient.ID));
            result.Add(currentRecipe);
        }
        return result;
    }

}

public abstract class FileOperation
{
    public List<string> ReadFromFile(string filePath = "recipe.txt")
    {
        string contentString = "";
        var data = new List<string>();
        if (File.Exists(filePath))
        {
            contentString = File.ReadAllText(filePath);
        }

        if (contentString != "")
        {
            data = ConvertToListFormat(contentString);
        }
        return data;
    }

    public void StoreToFile(List<string> strings, string filePath = "recipe.txt")
    {
        string fileString = ConvertToStringFormat(strings);
        File.WriteAllText(@filePath, fileString);
    }

    public abstract List<string> ConvertToListFormat(string @string);

    public abstract string ConvertToStringFormat(List<string> strings);

}

public class JsonFileOperation : FileOperation
{
    public override List<string> ConvertToListFormat(string @string)
    {
        return JsonSerializer.Deserialize<List<string>>(@string);
    }

    public override string ConvertToStringFormat(List<string> strings)
    {
        return JsonSerializer.Serialize(strings);
    }
}

public class TxtFileOperation : FileOperation
{
    public override List<string> ConvertToListFormat(string @string)
    {
        return @string.Split(Environment.NewLine).ToList();
    }

    public override string ConvertToStringFormat(List<string> strings)
    {
        return string.Join(Environment.NewLine, strings);
    }
}







