using Godot;
using System;

[GlobalClass]
public partial class RecipeBookData : Resource
{
    [Export] public Godot.Collections.Array<RecipeData> recipeData; // TODO should probably be dictionary for unique id per recipe

    public void AddRecipe(RecipeData data)
    {
        recipeData.Add(data);
    }
}


