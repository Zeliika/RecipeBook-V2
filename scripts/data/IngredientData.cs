using Godot;
using System;

[GlobalClass]
public partial class IngredientData : Resource
{
    [Export] public string ingredientName;

    [Export] public float baseQuantity;

    [Export] public GlobalTypes.Unit unit;

    [Export] public long recipeID;


    public IngredientData(string ingredientName, float baseQuantity, GlobalTypes.Unit unit, long recipeID)
    {
        this.ingredientName = ingredientName;
        this.baseQuantity = baseQuantity;
        this.unit = unit;
        this.recipeID = recipeID;
    }

    public IngredientData()
    {

    }


}
