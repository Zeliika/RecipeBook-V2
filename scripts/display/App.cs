using Godot;
using System;

public partial class App : VSplitContainer
{

    [Export] protected TabContainer TabContentContainer;
    public PackedScene RecipeScene = GD.Load<PackedScene>("uid://cqpd148u0ridy");
    public static RecipeBookData recipeBook;
    protected EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        eventBus.Connect("RecipeOpened", new Callable(this, MethodName.OnRecipeOpened));
        eventBus.Connect("RecipeClosed", new Callable(this, MethodName.OnRecipeClosed));
        eventBus.Connect("DataDisplayChangeRequested", new Callable(this, MethodName.OnDataDisplayChangeRequested));
        TabContentContainer.SetTabTitle(TabContentContainer.CurrentTab, "Rezeptliste");
    }

    protected void OnRecipeOpened(RecipeData data, bool editMode, bool newRecipe)
    {
        foreach (Node recipeDisplay in TabContentContainer.GetChildren())
        {
            if (recipeDisplay is not RecipeDisplay)
            {
                continue;
            }
            if (TabContentContainer.GetTabTitle(recipeDisplay.GetIndex()) == data.recipeName)
            {
                TabContentContainer.CurrentTab = recipeDisplay.GetIndex();
                return;
            }
        }
        RecipeDisplay recipe = (RecipeDisplay)RecipeScene.Instantiate();
        TabContentContainer.AddChild(recipe);
        TabContentContainer.SetTabTitle(recipe.GetIndex(), data.recipeName);
        recipe.Init(data);
        eventBus.EmitSignal(EventBus.SignalName.RecipeIsNew, newRecipe);
        TabContentContainer.CurrentTab = TabContentContainer.GetChildCount() - 1;
        if (editMode == true)
        {
            recipe.SetEditMode(true, data);

        }
    }
    protected void OnRecipeClosed()
    {
        TabContentContainer.GetChild(TabContentContainer.CurrentTab).QueueFree();
    }

    protected void OnTabSelected(int tab)
    {
        TabContentContainer.CurrentTab = tab;
    }

    protected void OnDataDisplayChangeRequested(RecipeData recipeData)
    {
        TabContentContainer.SetTabTitle(TabContentContainer.CurrentTab, recipeData.recipeName);
    }
    
}
