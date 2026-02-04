using Godot;
using System;
using System.Linq;

[GlobalClass]
public partial class RecipeBookData : Resource
{
    [Export] public Godot.Collections.Dictionary<int, RecipeData> recipeData;

    public void AddRecipe(RecipeData data)
    {
        recipeData.Add(data.recipeID, data);
    }

    public void RemoveRecipe(RecipeData data)
    {
        recipeData.Remove(data.recipeID);
        
    }
}


