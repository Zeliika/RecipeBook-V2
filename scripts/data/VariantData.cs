using Godot;
using System;

[GlobalClass]
public partial class VariantData : Resource
{
    [Export] public string variantName;
    [Export] public string variantDescription;
    [Export] public IngredientData[] ingredients;

    public VariantData(string variantName, IngredientData[] ingredients, string variantDescription)
    {
        this.variantName = variantName;
        this.ingredients = ingredients;
        this.variantDescription = variantDescription;
    }

    public VariantData()
    {

    }
}


