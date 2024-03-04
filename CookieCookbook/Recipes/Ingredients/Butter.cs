namespace CookieCookbook.Recipes.Ingredients
{
    public class Butter : Ingredient
    {
        public override int Id { get; } = 3;

        public override string Name { get; } = "Butter";

        public override string PrepareInstruction => $"Melt on low heat. {base.PrepareInstruction}";
    }

}

