using Godot;
using System;

public partial class IngredientData : Resource
{
    [Export] public string ingredient_name;

    [Export] public double base_quantity;

    [Export] GlobalTypes.Unit unit;


    public IngredientData(string ingredient_name = "", double base_quantity = 0.0, GlobalTypes.Unit unit = GlobalTypes.Unit.NONE)
    {
        this.ingredient_name = ingredient_name;
        this.base_quantity = base_quantity;
        this.unit = unit;

    }
    
    
}
