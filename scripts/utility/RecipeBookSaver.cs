using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

[GlobalClass]
public partial class RecipeBookSaver : ResourceFormatSaver
{
    public override string[] _GetRecognizedExtensions(Resource resource)
    {
        // return [];
        return ["rb"];
    }

    public override bool _Recognize(Resource resource)
    {
        return resource is RecipeBookData;
    }

    public override Error _Save(Resource resource, string path, uint flags)
    {
        FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        var saveFile = file.StoreString(Json.Stringify(GetSaveData((RecipeBookData)resource), "\t"));
        file.Dispose();
        return saveFile ? Error.Ok : Error.Failed;
        // return file.StoreString(Json.Stringify(GetSaveData((RecipeBookData)resource), "\t")) ? Error.Ok : Error.Failed;
    }

    protected Godot.Collections.Dictionary<string, Variant> GetSaveData(RecipeBookData recipeBookData)
    {
        var recipeBookDictionary = new Godot.Collections.Dictionary<string, Variant> { };
        Godot.Collections.Array<Godot.Collections.Dictionary<string, Variant>> recipes = new Godot.Collections.Array<Godot.Collections.Dictionary<string,Variant>>();
        foreach (RecipeData recipe in recipeBookData.recipeData.Values)
        {
            Godot.Collections.Dictionary<string, Variant> recipeDictionary = new Godot.Collections.Dictionary<string, Variant> { };
            recipeDictionary["recipe"] = recipe.recipeName;
            recipeDictionary["description"] = recipe.description;
            recipeDictionary["tags"] = recipe.tags;
            recipeDictionary["icon"] = "";
            recipeDictionary["recipe_id"] = recipe.recipeID;
            recipeDictionary["creation_date"] = recipe.recipeID;
            recipeDictionary["modification_date"] = recipe.lastEdited;
            var variants = new Godot.Collections.Array<Godot.Collections.Dictionary<string, Variant>>();

            foreach (VariantData variant in recipe.variants)
            {
                var variantDictionary = new Godot.Collections.Dictionary<string, Variant> { };
                variantDictionary["variant"] = variant.variantName;
                variantDictionary["variant_description"] = variant.variantDescription;
                Godot.Collections.Array<Godot.Collections.Dictionary<string, Variant>> ingredients = new Godot.Collections.Array<Godot.Collections.Dictionary<string, Variant>>();


                foreach (IngredientData ingredient in variant.ingredients)
                {
                    var ingredientDictionary = new Godot.Collections.Dictionary<string, Variant> { };
                    ingredientDictionary["ingredient"] = ingredient.ingredientName;
                    ingredientDictionary["base_quantity"] = ingredient.baseQuantity;
                    ingredientDictionary["unit"] = (int) ingredient.unit;
                    ingredientDictionary["recipeID"] = ingredient.recipeID;
                    ingredients.Add(ingredientDictionary);
                }
                variantDictionary["ingredients"] = ingredients;
                variants.Add(variantDictionary);
            }
            recipeDictionary["variants"] = variants;
            recipes.Add(recipeDictionary);
        }
        recipeBookDictionary["recipes"] = recipes;

        // GD.Print(recipeBookDictionary);

        return recipeBookDictionary;
    }
}
