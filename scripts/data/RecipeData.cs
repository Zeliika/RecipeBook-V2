using Godot;
using System;

public partial class RecipeData : Resource
{
    [Export] string recipe_name;
    [Export] Texture2D texture;
    [Export] GlobalTypes.Tag tags;
    string description;




    public RecipeData()
    {
        

    }
}
