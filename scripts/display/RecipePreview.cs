using Godot;
using System;

public partial class RecipePreview : Button
{
    [Export] public TextureRect PlaceHolderImage;
    [Export] public Label RecipeNamePreview;
    [Export] public HFlowContainer TagPreviewContainer;
    public PackedScene TagDisplayScene = GD.Load<PackedScene>("uid://b01ict1hfluwg");

    public RecipeData recipeData;
    public void Init(RecipeData recipeData)
    {
        this.recipeData = recipeData;
        //PlaceHolderImage.Texture = recipeData.texture;
        RecipeNamePreview.Text = recipeData.recipeName;
        foreach (GlobalTypes.Tag tag in recipeData.tags)
        {
            TagDisplay display = (TagDisplay)TagDisplayScene.Instantiate();
            TagPreviewContainer.AddChild(display);
            display.Init(GlobalTypes.Tags[tag]);
        }
    }
}
