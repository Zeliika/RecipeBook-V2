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

    public Dictionary<int, SelectedTagDisplay> TagSelection = new Dictionary<int, SelectedTagDisplay>{};
    public PackedScene RecipePreviewScene = GD.Load<PackedScene>("uid://w1aq4pvqbg8d");
    public PackedScene SelectedTagScene = GD.Load<PackedScene>("uid://cjc7o8w424mm5");




    public override void _Ready()
    {
        OptionsMenuButton.GetPopup().Connect("id_pressed", new Callable(this, MethodName.OnIDPressed));
        TagSelector.GetPopup().HideOnCheckableItemSelection = false;
        foreach (GlobalTypes.Tag tag in GlobalTypes.Tags.Keys)
        {
            TagSelector.GetPopup().AddCheckItem(GlobalTypes.Tags[tag]);
            SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagScene.Instantiate();
            SelectedTagsContainer.AddChild(selection);
            selection.Init(GlobalTypes.Tags[tag]);
            selection.Visible = false;
            selection.Connect("OnTagRemoved", new Callable(this, MethodName.OnTagRemovedSignalReceived));
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
        TagSelector.GetPopup().SetItemChecked(id, (!TagSelector.GetPopup().IsItemChecked(id)));
        
        SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagsContainer.GetChild(id);
        selection.SetVisible(TagSelector.GetPopup().IsItemChecked(id));
    }

    protected void OnTagRemovedSignalReceived(int id)
    {
        TagSelector.GetPopup().SetItemChecked(id, (!TagSelector.GetPopup().IsItemChecked(id)));

    }

    protected void ClearSearch()
    {
        TextSearchField.Text = "";
        for (int item = 0; item < TagSelector.GetPopup().ItemCount; item++)
        {
            TagSelector.GetPopup().SetItemChecked(item, false);
        }
    }

    public void StartSearch(string searchText)
    {
        string searchTextInput = TextSearchField.Text;

        GlobalTypes.Tag[] SelectedTags;
        
        // for (int item = 0; item < TagSelector.GetPopup().ItemCount; item++)
        // {
        //     if (TagSelector.GetPopup().IsItemChecked(item))
        //     {
        //         SelectedTags.Append(TagSelector.GetPopup().GetItemId(item))
        //     }
        // }
    }

}
