// See https://aka.ms/new-console-template for more information
using CookieCookbook.Recipes;
using CookieCookbook.Recipes.Ingredients;
using CookieCookbook.DataAccess;
using CookieCookbook.FileAccess;
using CookieCookbook.App;

try
{
    const FileFormat Format = FileFormat.Json;

    IStringsRepository stringsRepository = Format == FileFormat.Json ?
    new StringsJsonRepository() :
    new StringsTextualRepository();

    string fileName = "recipe";
    var fileMetaData = new FileMetaData(fileName, Format);
    var filePath = fileMetaData.ToPath();

    var ingredientRegister = new IngredientRegister();
    var cookBookApp = new CookBookApp(new RecipesRepository(stringsRepository, ingredientRegister),
    new RecipesConsoleUserInteraction(ingredientRegister));

    cookBookApp.Run(filePath);

}
catch (Exception ex)
{
    System.Console.WriteLine("Sorry! The application is experiencing an unexpected error," +
    "and will have to be closed." +
    "Error Message:" + ex.Message);
}

