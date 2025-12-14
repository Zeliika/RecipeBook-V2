using Godot;
using System;

public partial class VariantDisplay : Control
{
    [Export] public Label RecipeDescription;
    [Export] public Label VariantDescription;
    [Export] public VBoxContainer IngredientListContainer;

    public PackedScene IngredientDisplayScene = GD.Load<PackedScene>("uid://ckghx552h32nh");

    protected VariantData variantData;

    public void Init(VariantData variantData, string recipeDescription)
    {
        this.variantData = variantData;
        Name = variantData.variantName;
        RecipeDescription.Text = recipeDescription;
        VariantDescription.Text = variantData.variantDescription;
        
        foreach (IngredientData ingredient in variantData.ingredients)
        {
            IngredientDisplay display = (IngredientDisplay)IngredientDisplayScene.Instantiate();
            IngredientListContainer.AddChild(display);
            display.Init(ingredient);
        }
    }
}
