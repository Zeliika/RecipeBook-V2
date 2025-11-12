using Godot;
using System;

public partial class VariantData : Resource
{
    public string variant_name;
    public IngredientData[] ingredients;
    public string variant_description;

    public VariantData(string variant_name = "", IngredientData[] ingredients = [], string variant_description = "")
    {
        this.variant_name = variant_name;
        this.ingredients = ingredients;
        this.variant_description = variant_description;
    }
}


