using Godot;
using System;

[GlobalClass]
public partial class IngredientData : Resource
{
    [Export] public string ingredientName;

    [Export] public double baseQuantity;

    [Export] public GlobalTypes.Unit unit;


    public IngredientData(string ingredientName, double baseQuantity, GlobalTypes.Unit unit)
    {
        this.ingredientName = ingredientName;
        this.baseQuantity = baseQuantity;
        this.unit = unit;
    }

    public IngredientData()
    {

    }


}
