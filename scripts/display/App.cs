using Godot;
using System;

public partial class App : VSplitContainer
{

    protected EventBus eventBus;

    [Export] protected TabContainer TabContentContainer;
    [Export] protected TabBar TabHeaderContainer;
    public PackedScene RecipeScene = GD.Load<PackedScene>("uid://cqpd148u0ridy");

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        eventBus.Connect("RecipeOpened", new Callable(this, MethodName.OnRecipeOpened));
        eventBus.Connect("RecipeClosed", new Callable(this, MethodName.OnRecipeClosed));
    }

    protected void OnRecipeOpened(RecipeData data, bool editMode)
    {
        for (int tab = 1; tab < TabHeaderContainer.TabCount; tab++)
        {
            if (TabHeaderContainer.GetTabTitle(tab) == data.recipeName)
            {
                TabContentContainer.CurrentTab = tab;
                TabHeaderContainer.CurrentTab = tab;
                return;
            }
        }
        RecipeDisplay recipe = (RecipeDisplay)RecipeScene.Instantiate();
        TabContentContainer.AddChild(recipe);
        recipe.Init(data);
        TabHeaderContainer.AddTab(data.recipeName);
        TabHeaderContainer.CurrentTab = TabHeaderContainer.TabCount - 1;
        TabContentContainer.CurrentTab = TabHeaderContainer.TabCount - 1;
        if (editMode == true)
        {
            recipe.SetEditMode(true, data);
            
        }
    }
    protected void OnRecipeClosed()
    {
        TabContentContainer.GetChild(TabContentContainer.CurrentTab).QueueFree();
        TabHeaderContainer.RemoveTab(TabHeaderContainer.CurrentTab);
    }

    protected void OnTabSelected(int tab)
    {
        TabContentContainer.CurrentTab = tab;
    }
    
}
