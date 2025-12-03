using Godot;
using System;
using System.Collections.Generic;

public partial class RecipeBookDisplay : Control
{
    [Export] LineEdit TextSearchField;
    [Export] VBoxContainer RecipeList;
    [Export] Button ClearSeachButton;
    [Export] MenuButton OptionsMenuButton;

    [Export] MenuButton TagSelector;
    [Export] HFlowContainer SelectedTagsContainer;
    [Export] public RecipeBookData recipeBookData; // TESTING just for testing remove later

    public HashSet<GlobalTypes.Tag> TagSelection = new HashSet<GlobalTypes.Tag>{};
    public PackedScene RecipePreviewScene = GD.Load<PackedScene>("uid://w1aq4pvqbg8d");
    public PackedScene SelectedTagScene = GD.Load<PackedScene>("uid://cjc7o8w424mm5");




    public override void _Ready()
    {
        OptionsMenuButton.GetPopup().Connect("id_pressed", new Callable(this, MethodName.OnIDPressed));
        TagSelector.GetPopup().HideOnCheckableItemSelection = false;
        foreach (GlobalTypes.Tag tag in GlobalTypes.Tags.Keys)
        {
            TagSelector.GetPopup().AddCheckItem(GlobalTypes.Tags[tag], (int)tag);
            SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagScene.Instantiate();
            SelectedTagsContainer.AddChild(selection);
            selection.Init(GlobalTypes.Tags[tag]);
            selection.Visible = false;
            // selection.Connect("OnTagRemoved", new Callable(this, MethodName.OnTagRemovedSignalReceived));
            selection.Connect("OnTagRemoved", Callable.From(()=>OnTagRemovedSignalReceived((int) tag)));
        }
        TagSelector.GetPopup().Connect("id_pressed", new Callable(this, MethodName.OnTagSelected));

        Init(recipeBookData); // TESTING just for testing

    }

    public void Init(RecipeBookData recipeBookData)
    {
        this.recipeBookData = recipeBookData;
        foreach (RecipeData recipe in recipeBookData.recipeData)
        {
            RecipePreview display = (RecipePreview)RecipePreviewScene.Instantiate();
            RecipeList.AddChild(display);
            display.Init(recipe);
        }
    }

    protected void OnIDPressed(int id)
    {
        switch (id)
        {
            case 0:
                GD.Print("Add");
                break;

            case 1:
                GD.Print("Select");
                break;
            default:
                return;

        }
    }

    protected void OnTagSelected(int id)
    {
        int index = TagSelector.GetPopup().GetItemIndex(id);
        TagSelector.GetPopup().SetItemChecked(index, (!TagSelector.GetPopup().IsItemChecked(index)));

        SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagsContainer.GetChild(index);
        selection.SetVisible(TagSelector.GetPopup().IsItemChecked(index));
        if (TagSelector.GetPopup().IsItemChecked(index))
        {
            TagSelection.Add((GlobalTypes.Tag)id);
        }
        else
        {
            TagSelection.Remove((GlobalTypes.Tag)id);
        }
        StartSearch();
    }

    protected void OnTagRemovedSignalReceived(int id)
    {
        int index = TagSelector.GetPopup().GetItemIndex(id);
        TagSelector.GetPopup().SetItemChecked(index, (!TagSelector.GetPopup().IsItemChecked(index)));
        TagSelection.Remove((GlobalTypes.Tag)id);
        StartSearch();
    }

    protected void ClearSearch()
    {
        TextSearchField.Text = "";
        for (int item = 0; item < TagSelector.GetPopup().ItemCount; item++)
        {
            TagSelector.GetPopup().SetItemChecked(item, false);
            SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagsContainer.GetChild(item);
            selection.Visible = false;
        }
        foreach (RecipePreview recipe in RecipeList.GetChildren())
        {
            recipe.Visible = true;
        }
        
    }

    protected string ProcessString(string input)
    {
        return input.ToLower().Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("/", " ");
    }

    protected void StartSearch()
    {
        foreach (RecipePreview recipe in RecipeList.GetChildren())
        {
            RecipeData recipeData = recipe.recipeData;            
            recipe.Visible = SearchRecipes(recipeData, ProcessString(TextSearchField.Text));
        }

    }
    protected bool SearchRecipes(RecipeData recipeData, string searchText)
    {
        foreach (GlobalTypes.Tag tag in TagSelection)
        {
            if (!recipeData.tags.Contains(tag))
            {
                return false;
            }
        }
        if (searchText == "" || ProcessString(recipeData.recipeName).Contains(searchText))
        {
            return true;
        }
            foreach (VariantData variant in recipeData.variants)
        {
            if (ProcessString(variant.variantName).Contains(searchText))
            {
                return true;
            }
            foreach (IngredientData ingredient in variant.ingredients)
            {
                if (ProcessString(ingredient.ingredientName).Contains(searchText))
                {
                    return true;
                }
            }
        }
        return false;
    }

   

}
