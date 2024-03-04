namespace CookieCookbook.Recipes;

public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);

    void Write(string filePat, IEnumerable<Recipe> allRecipes);

}

