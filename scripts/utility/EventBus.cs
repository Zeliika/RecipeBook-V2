using Godot;
using System;

[GlobalClass]
public partial class EventBus : Node
{

    [Signal]
    public delegate void TestEventHandler();


    [Signal]
    public delegate void RecipeClosedEventHandler();

    [Signal]
    public delegate void RecipeOpenedEventHandler(RecipeData recipeData, bool editMode);

    [Signal]
    public delegate void IngredientDeletedEventHandler(int index);
    [Signal]
    public delegate void QuantityTextFieldEditedEventHandler(bool isFloat);
}
