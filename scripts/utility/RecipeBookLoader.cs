using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;


[GlobalClass]
public partial class RecipeBookLoader : ResourceFormatLoader
{
    public override string[] _GetRecognizedExtensions()
    {
        return ["rb"];
    }

    public override string _GetResourceType(string path)
    {
        if (path.GetExtension() == "rb")
        {
            return "Resource";
        }
        return "";
    }

    public override string _GetResourceScriptClass(string path)
    {
        if (path.GetExtension() == "rb")
        {
            return "RecipeBookData";
        }
        return "";
    }

    public override Variant _Load(string path, string originalPath, bool useSubThreads, int cacheMode)
    {
        Json json = new Json();
        FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        var text = file.GetAsText();
        var parseResult = json.Parse(text);
        if (parseResult == Error.Ok)
        {
            return LoadRecipeBook((Godot.Collections.Dictionary<string, Variant>)json.Data);
        }
        return new RecipeBookData();
    }


    public override bool _HandlesType(StringName type)
    {
        return type == "Resource";
    }


    protected RecipeBookData LoadRecipeBook(Godot.Collections.Dictionary<string, Variant> recipeBookDictionary)
    {
        RecipeBookData recipeBookData = new RecipeBookData();
        var recipes = new Godot.Collections.Dictionary<long,RecipeData>();

        foreach (var recipeDictionary in (Godot.Collections.Array<Godot.Collections.Dictionary<string, Variant>>)recipeBookDictionary["recipes"])
        {
            RecipeData recipeData = new RecipeData();
            recipeData.recipeName = (string)recipeDictionary["recipe"];
            recipeData.tags = (Godot.Collections.Array<GlobalTypes.Tag>)recipeDictionary["tags"];
            recipeData.description = (string)recipeDictionary["description"];
            recipeData.recipeID = (long)recipeDictionary["recipe_id"];
            recipeData.lastEdited = (long)recipeDictionary["modification_date"];
            Godot.Collections.Array<VariantData> variants = new Godot.Collections.Array<VariantData>();
            foreach (var variantDictionary in (Godot.Collections.Array<Godot.Collections.Dictionary<string, Variant>>)recipeDictionary["variants"])
            {
                VariantData variantData = new VariantData();
                variantData.variantName = (string)variantDictionary["variant"];
                variantData.variantDescription = (string)variantDictionary["variant_description"];
                Godot.Collections.Array<IngredientData> ingredients = new Godot.Collections.Array<IngredientData>();
                foreach (var ingredientDictionary in (Godot.Collections.Array<Godot.Collections.Dictionary<string, Variant>>)variantDictionary["ingredients"])
                {
                    IngredientData ingredientData = new IngredientData();
                    ingredientData.ingredientName = (string)ingredientDictionary["ingredient"];
                    ingredientData.baseQuantity = (float)ingredientDictionary["base_quantity"];
                    ingredientData.unit = (GlobalTypes.Unit)(int)ingredientDictionary["unit"];
                    ingredientData.recipeID = (long)ingredientDictionary["recipeID"];
                    ingredients.Add(ingredientData);
                }
                variantData.ingredients = ingredients;
                variants.Add(variantData);
            }
            recipeData.variants = variants;
            recipes.Add(recipeData.recipeID,recipeData);
        }
        recipeBookData.recipeData = recipes;
        return recipeBookData;
    }


}
