using Godot;
using System;

[GlobalClass]
public partial class RecipeData : Resource
{
    [Export] public string recipeName = "";
    [Export] public string description = "";
    [Export] public Texture2D texture;
    [Export] public Godot.Collections.Array<GlobalTypes.Tag> tags = new Godot.Collections.Array<GlobalTypes.Tag>();
    [Export] public Godot.Collections.Array<VariantData> variants = new Godot.Collections.Array<VariantData>();




    public RecipeData(string recipeName, Godot.Collections.Array<VariantData> variants, Godot.Collections.Array<GlobalTypes.Tag> tags, string description)
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
