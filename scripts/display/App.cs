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
        // eventBus.Connect("Test", new Callable(this, MethodName.OnTest));
        // eventBus.EmitSignal(EventBus.SignalName.Test);
        eventBus.Connect("RecipeOpened", new Callable(this, MethodName.OnRecipeOpened));
        eventBus.Connect("RecipeClosed", new Callable(this, MethodName.OnRecipeClosed));
    }


    // void OnTest()
    // {
    //     GD.Print("Test");
    // }


    protected void OnRecipeOpened(RecipeData data)
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
        //TODO 
        //make child recipe display of tabContenContainer
        //make toggle button child of tabHeader Container -> set buttonGroup
        //activate correct index in TabContainer -> should actually be handled by button being pressed
        //activate correct button (others should unpress)

    }
    protected void OnRecipeClosed()
    {
        TabContentContainer.GetChild(TabContentContainer.CurrentTab).QueueFree();
        //TabContentContainer.CurrentTab = 0;
        TabHeaderContainer.RemoveTab(TabHeaderContainer.CurrentTab);
        //TabHeaderContainer.CurrentTab = 0;
        //TODO 
        //remove child recipe display of tabContenContainer
        //remove toggle button child of tabHeader Container -> set buttonGroup
        //activate correct button (probably recipe book??) -> should automatically opern correct tab

    }

    //TODO
    //function to handle tab button pressed -> activate correct tab in TabContentContainer

    protected void OnTabSelected(int tab)
    {
        TabContentContainer.CurrentTab = tab;
    }
    
}
