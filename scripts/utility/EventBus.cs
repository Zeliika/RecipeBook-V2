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
    public delegate void RecipeOpenedEventHandler(RecipeData recipeData, bool editMode, bool newRecipe);

    [Signal]
    public delegate void IngredientDeletedEventHandler(int index);

    [Signal]
    public delegate void QuantityTextFieldEditedEventHandler(bool isFloat);

    [Signal]
    public delegate void SaveRequestedEventHandler();

    [Signal]
    public delegate void DeleteRecipeRequestedEventHandler(int recipeID);

    [Signal]
    public delegate void DataDisplayChangeRequestedEventHandler(RecipeData recipeData);

    [Signal]
    public delegate void RecipeIsNewEventHandler(bool newRecipe);

}
