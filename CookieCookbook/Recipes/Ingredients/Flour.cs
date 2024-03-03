namespace CookieCookbook.Recipes.Ingredients
{
    public abstract class Flour : Ingredient
    {
        public override string PrepareInstruction => $"Sieve. {base.PrepareInstruction}";
    }

}

