using Godot;
using System;
using System.Collections.Generic;


public partial class RecipeDisplay : Control
{
    [Export] Label RecipeNameLabel;
    [Export] HFlowContainer TagContainer;
    [Export] MenuButton OptionsButton;
    [Export] TabContainer VariantContainer;

    [Export] Button CloseRecipeButton;

    [Export] LineEdit TitleEdit;
    [Export] TextEdit DescriptionEdit;
    [Export] MenuButton TagSelector;
    [Export] HBoxContainer TagEditContainer;
    [Export] HFlowContainer SelectedTagsContainer;
    [Export] Label DescriptionLabel;
    [Export] Button AddVariantButton;
    [Export] Button CancelEditButton;
    [Export] Button ResetEditButton;
    [Export] Button SaveEditButton;
    [Export] ScrollContainer DescriptionEditScrollContainer;
    [Export] protected Panel AddVariant;

    protected HashSet<GlobalTypes.Tag> TagSelection = new HashSet<GlobalTypes.Tag> { };

    protected PackedScene SelectedTagScene = GD.Load<PackedScene>("uid://cjc7o8w424mm5");

    public RecipeData recipeData;
    protected PackedScene TagDisplayScene = GD.Load<PackedScene>("uid://b01ict1hfluwg");
    protected PackedScene VariantDisplayScene = GD.Load<PackedScene>("uid://6oj7ail07oed");

    protected EventBus eventBus;
    protected bool editModeActive;


    public override void _Ready()
    {
        OptionsButton.GetPopup().Connect("id_pressed", new Callable(this, MethodName.OnItemSelected));
        eventBus = GetNode<EventBus>("/root/EventBus");
        TagSelector.GetPopup().HideOnCheckableItemSelection = false;
        foreach (GlobalTypes.Tag tag in GlobalTypes.Tags.Keys)
        {
            TagSelector.GetPopup().AddCheckItem(GlobalTypes.Tags[tag], (int)tag);
            SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagScene.Instantiate();
            SelectedTagsContainer.AddChild(selection);
            selection.Init(GlobalTypes.Tags[tag]);
            selection.Visible = false;
            selection.Connect("OnTagRemoved", Callable.From(() => OnTagRemovedSignalReceived((int)tag)));
        }
        TagSelector.GetPopup().Connect("id_pressed", new Callable(this, MethodName.OnTagSelected));
        VariantContainer.SetTabHidden(0, true);
        eventBus.Connect("QuantityTextFieldEdited", new Callable(this, MethodName.OnQuantityTextFieldEdited));
    }

    public void Init(RecipeData recipeData)
    {
        GD.Print(DateTime.Now.Ticks);
        this.recipeData = recipeData;
        RecipeNameLabel.Text = recipeData.recipeName;
        foreach (GlobalTypes.Tag tag in recipeData.tags)
        {
            TagDisplay display = (TagDisplay)TagDisplayScene.Instantiate();
            TagContainer.AddChild(display);
            display.Init(GlobalTypes.Tags[tag]);

            int index = TagSelector.GetPopup().GetItemIndex((int)tag);
            SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagsContainer.GetChild(index);
            selection.Visible = true;
            TagSelector.GetPopup().SetItemChecked(index, (!TagSelector.GetPopup().IsItemChecked(index)));
            if (TagSelector.GetPopup().IsItemChecked(index))
            {
                TagSelection.Add(tag);
            }
            else
            {
                TagSelection.Remove(tag);
            }
        }
        foreach (VariantData variant in recipeData.variants)
        {
            CreateVariantTab(variant);
        }
        VariantContainer.CurrentTab = 0;
    }

    protected void OnCloseRecipeButtonPressed()
    {
        eventBus.EmitSignal(EventBus.SignalName.RecipeClosed);
    }

    protected void OnItemSelected(int id)
    {
        switch (id)
        {
            case 0:
                SetEditMode(true, recipeData);
                break;
            case 1:
                GD.Print("Delete");
                break;
            case 2:
                GD.Print("Export");
                break;
            default:
                return;
        }
    }

    public void SetEditMode(bool editing, RecipeData recipeData)
    {
        editModeActive = editing;
        SetEditModeLayout(editing);
        TitleEdit.Text = recipeData.recipeName;
        DescriptionEdit.Text = recipeData.description;
        VariantContainer.SetTabHidden(VariantContainer.GetChildCount() - 1, false);
        foreach (Node variantDisplay in VariantContainer.GetChildren())
        {
            if (!(variantDisplay is VariantDisplay))
            {
                continue;
            }
            ((VariantDisplay)variantDisplay).SetEditMode(editing);
        }
    }

    protected void SetEditModeLayout(bool editing)
    {
        CloseRecipeButton.Visible = !editing;
        RecipeNameLabel.Visible = !editing;
        OptionsButton.Visible = !editing;
        CancelEditButton.Visible = editing;
        TitleEdit.Visible = editing;
        ResetEditButton.Visible = editing;
        SaveEditButton.Visible = editing;
        TagContainer.Visible = !editing;
        TagEditContainer.Visible = editing;
        SelectedTagsContainer.Visible = editing;
        DescriptionLabel.Visible = editing;
        DescriptionEditScrollContainer.Visible = editing;
        VariantContainer.SetTabHidden(VariantContainer.GetChildCount() - 1, !editing);
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
    }

    protected void OnTagRemovedSignalReceived(int id)
    {
        int index = TagSelector.GetPopup().GetItemIndex(id);
        TagSelector.GetPopup().SetItemChecked(index, (!TagSelector.GetPopup().IsItemChecked(index)));
        TagSelection.Remove((GlobalTypes.Tag)id);
    }

    protected void OnResetEditButtonPressed()
    {
        TitleEdit.Text = recipeData.recipeName;
        DescriptionEdit.Text = recipeData.description;
        ResetTags();
        foreach (Node variantDisplay in VariantContainer.GetChildren())
        {
            if (!(variantDisplay is VariantDisplay))
            {
                continue;
            }
            ((VariantDisplay)variantDisplay).ResetEditMode();
        }
    }

    protected void OnCancelEditButtonPressed()
    {
        editModeActive = false;
        SetEditModeLayout(editModeActive);
        ResetTags();
        foreach (Node variantDisplay in VariantContainer.GetChildren())
        {
            if (!(variantDisplay is VariantDisplay))
            {
                continue;
            }
            ((VariantDisplay)variantDisplay).CancelEditMode(editModeActive);
        }
    }


    protected void ResetTags()
    {
        foreach (GlobalTypes.Tag tag in GlobalTypes.Tags.Keys)
        {
            int index = TagSelector.GetPopup().GetItemIndex((int)tag);
            SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagsContainer.GetChild(index);
            selection.Visible = false;
            TagSelector.GetPopup().SetItemChecked(index, false);
            TagSelection.Clear();
        }

        foreach (GlobalTypes.Tag tag in recipeData.tags)
        {
            int index = TagSelector.GetPopup().GetItemIndex((int)tag);
            SelectedTagDisplay selection = (SelectedTagDisplay)SelectedTagsContainer.GetChild(index);
            selection.Visible = true;
            TagSelector.GetPopup().SetItemChecked(index, (!TagSelector.GetPopup().IsItemChecked(index)));
            if (TagSelector.GetPopup().IsItemChecked(index))
            {
                TagSelection.Add(tag);
            }
            else
            {
                TagSelection.Remove(tag);
            }
        }
    }

    protected void OnAddVariantPressed(int index)
    {
        if (!editModeActive)
        {
            return;
        }
        if (index == VariantContainer.GetChildCount() - 1)
        {
            CreateVariantTab(new VariantData()).SetEditMode(true);
            VariantContainer.CurrentTab = VariantContainer.GetChildCount() - 2;
        }
    }

    protected VariantDisplay CreateVariantTab(VariantData data)
    {
        VariantDisplay display = (VariantDisplay)VariantDisplayScene.Instantiate();
        VariantContainer.AddChild(display);
        VariantContainer.MoveChild(display, VariantContainer.GetChildCount() - 2);
        VariantContainer.SetTabTitle(display.GetIndex(), data.variantName);
        display.Init(data, recipeData.description);
        return display;
    }

    protected void OnSaveEditButtonPressed()
    {
        recipeData.recipeName = TitleEdit.GetText();
        recipeData.description = DescriptionEdit.GetText();
        recipeData.variants.Clear();
        foreach (Node variant in VariantContainer.GetChildren())
        {
            if (!(variant is VariantDisplay))
            {
                continue;
            }
            recipeData.variants.Add(((VariantDisplay)variant).ApplyChanges());
        }
        //Update RecipeData
        //get VariantData with variantDisplay.ApplyChanges
        //Update RecipeBookData
        //Save RecipeBookData
        //set Edit Mode false here and in variantDisplay
    }

    protected void OnQuantityTextFieldEdited(bool isFloat)
    {
        SaveEditButton.Disabled = !isFloat;
    }


}
