namespace CookieCookbook.Recipes.Ingredients
{
    public abstract class Spice : Ingredient
    {
        public override string PrepareInstruction => $"Take half a teaspoon. {base.PrepareInstruction}";
    }

}

