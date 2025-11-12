using Godot;
using System;

public partial class RecipeDisplay : Control
{
    [Export] Label RecipeNameLabel;
    [Export] HFlowContainer TagContainer;

    [Export] MenuButton OptionsButton;

    protected PackedScene IngredientDisplayScene = GD.Load<PackedScene>("uid://ckghx552h32nh");

    protected PackedScene TagDisplayScene = GD.Load<PackedScene>("uid://b01ict1hfluwg");

    protected PackedScene VariantDisplayScene = GD.Load<PackedScene>("uid://6oj7ail07oed");
    public override void _Ready()
    {
        OptionsButton.GetPopup().Connect("id_pressed", new Callable(this, MethodName.OnItemSelected));
    }



    protected void ReturnToMenuButton()
    {
        GD.Print("Button pressed");
    }

    protected void OnItemSelected(int id)
    {

        if (id == 0)
        {
            GD.Print("Edit");
        }

        if (id == 1)
        {
            GD.Print("Delete");
        }

        if (id == 2)
        {
            GD.Print("Export");
        }
     
        
    }

}
