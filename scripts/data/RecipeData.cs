using Godot;
using System;

[GlobalClass]
public partial class RecipeData : Resource
{
    [Export] public string recipeName;
    [Export] public string description;
    [Export] public Texture2D texture;
    [Export] public Godot.Collections.Array<GlobalTypes.Tag> tags;
    [Export] public VariantData[] variants;




    public RecipeData(string recipeName, VariantData[] variants, Godot.Collections.Array<GlobalTypes.Tag> tags, string description)
    {
        this.recipeName = recipeName;
        this.tags = tags;
        this.variants = variants;
        this.description = description;
    }

    public RecipeData()
    {

    }
}
