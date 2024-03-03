// See https://aka.ms/new-console-template for more information
using System.Net.NetworkInformation;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;



var cookBook = new CookBook();
cookBook.PrintExistingRecipes();
Console.WriteLine("Create a new cookie recipe! Available ingredients are:");
cookBook.PrintAvailableIngredients();
cookBook.CreateNewRecipe();


public class CookBook
{
    public List<Recipe> Recipes { get; } = new List<Recipe>();

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

    private string _filePath = "recipe.txt";
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


public class Recipe
{
    public List<Ingredient> Ingredients { get; }

    public Recipe(List<Ingredient> ingredientLists)
    {
        Ingredients = ingredientLists;
    }

    public void PrintSingleRecipe()
    {
        var result = "";
        for (int i = 0; i < Ingredients.Count; i++)
        {
            result = result + $"{Ingredients[i].Name}. {Ingredients[i].PrepareInstruction}{Environment.NewLine}";
        }
        Console.WriteLine(result);
    }
}

public abstract class Ingredient
{

    public abstract int ID { get; }

    public virtual string Name => "some ingredient";

    public abstract string PrepareInstruction { get; }
}

public class WheatFlour : Ingredient
{
    public override int ID { get; } = 1;

    public override string Name { get; } = "Wheat flour";

    public override string PrepareInstruction { get; } = "Sieve. Add to other ingredients.";
}

public class CoconutFlour : Ingredient
{
    public override int ID { get; } = 2;

    public override string Name { get; } = "Coconut flour";

    public override string PrepareInstruction { get; } = "Sieve. Add to other ingredients.";
}

public class Butter : Ingredient
{
    public override int ID { get; } = 3;

    public override string Name { get; } = "Butter";

    public override string PrepareInstruction { get; } = "Melt on low heat. Add to other ingredients.";
}

public class Chocolate : Ingredient
{
    public override int ID { get; } = 4;

    public override string Name { get; } = "Chocolate";

    public override string PrepareInstruction { get; } = "Melt in a water bath. Add to other ingredients.";
}

public class Sugar : Ingredient
{
    public override int ID { get; } = 5;

    public override string Name { get; } = "Sugar";

    public override string PrepareInstruction { get; } = "Add to other ingredients.";
}

public class Cardamom : Ingredient
{
    public override int ID { get; } = 6;

    public override string Name { get; } = "Cardamom";

    public override string PrepareInstruction { get; } = "Take half a teaspoon. Add to other ingredients.";
}

public class Cinnamon : Ingredient
{
    public override int ID { get; } = 7;

    public override string Name { get; } = "Cinnamon";

    public override string PrepareInstruction { get; } = "Take half a teaspoon. Add to other ingredients.";
}

public class CocoaPowder : Ingredient
{
    public override int ID { get; } = 8;

    public override string Name { get; } = "Cocoa powder";

    public override string PrepareInstruction { get; } = "Add to other ingredients.";
}




