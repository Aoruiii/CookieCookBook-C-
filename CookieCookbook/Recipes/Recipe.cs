using CookieCookbook.Recipes.Ingredients;

namespace CookieCookbook.Recipes
{
    public class Recipe
    {
        public IEnumerable<Ingredient> Ingredients { get; }

        public Recipe(IEnumerable<Ingredient> ingredients)
        {
            Ingredients = ingredients;
        }

        public void PrintSingleRecipe()
        {
            var result = "";
            foreach (Ingredient ingredient in Ingredients)
            {
                result = result + $"{ingredient.Name}. {ingredient.PrepareInstruction}{Environment.NewLine}";
            }
            Console.WriteLine(result);
        }
    }

}

