using Godot;
using System;

public partial class App : VSplitContainer
{

    protected EventBus eventBus;

    [Export] protected TabContainer TabContentContainer;
    public PackedScene RecipeScene = GD.Load<PackedScene>("uid://cqpd148u0ridy");

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        eventBus.Connect("RecipeOpened", new Callable(this, MethodName.OnRecipeOpened));
        eventBus.Connect("RecipeClosed", new Callable(this, MethodName.OnRecipeClosed));
    }

    protected void OnRecipeOpened(RecipeData data, bool editMode)
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
    
}
