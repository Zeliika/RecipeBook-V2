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
    public delegate void RecipeOpenedEventHandler(RecipeData recipeData);

    //TODO
    //Signal RecipeOpened
    //Signal RecipeClosed -> emitted from recipe display when x button pressed, should provide some kind of id to know which tab to close
}
