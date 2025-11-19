using Godot;
using System;

public partial class RecipeDisplay : Control
{
    [Export] Label RecipeNameLabel;
    [Export] HFlowContainer TagContainer;
    [Export] MenuButton OptionsButton;
    [Export] TabContainer VariantContainer;

    public RecipeData recipeData = GD.Load<RecipeData>("uid://bip42grtxnc75"); // TESTING just for testing remove later
    public PackedScene IngredientDisplayScene = GD.Load<PackedScene>("uid://ckghx552h32nh");
    public PackedScene TagDisplayScene = GD.Load<PackedScene>("uid://b01ict1hfluwg");
    public PackedScene VariantDisplayScene = GD.Load<PackedScene>("uid://6oj7ail07oed");


    public override void _Ready()
    {
        OptionsButton.GetPopup().Connect("id_pressed", new Callable(this, MethodName.OnItemSelected));
        Init(recipeData); //TESTING remove when recipe book implemented
    }

    public void Init(RecipeData recipeData)
    {
        this.recipeData = recipeData;

        RecipeNameLabel.Text = recipeData.recipeName;

        foreach (GlobalTypes.Tag tag in recipeData.tags)
        {
            TagDisplay display = (TagDisplay)TagDisplayScene.Instantiate();
            TagContainer.AddChild(display);
            display.Init(GlobalTypes.Tags[tag]);
        }

        foreach (VariantData variant in recipeData.variants)
        {
            VariantDisplay display = (VariantDisplay)VariantDisplayScene.Instantiate();
            VariantContainer.AddChild(display);
            display.Init(variant, recipeData.description);
        }
        
    }



    protected void ReturnToMenuButton()
    {
        GD.Print("Button pressed");
    }

    protected void OnItemSelected(int id)
    {
        switch (id)
        {
            case 0:
                GD.Print("Edit");
                break;
            case 1:
                GD.Print("Delete");
                break;
            case 2:
                GD.Print("Export");
                break;
            default:
                return;
            
        }
        
    }

}
