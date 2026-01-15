using Godot;
using System;
using System.Linq;

public partial class VariantDisplay : Control
{
    [Export] public Label RecipeDescriptionLabel;
    [Export] public Label RecipeDescription;
    [Export] public Label VariantDescription;
    [Export] public Tree IngredientList;
    [Export] public Button AddIngredientButton;
    [Export] public TextEdit VariantNameEdit;
    [Export] public TextEdit VariantDescriptionEdit;
    [Export] public Texture2D RemoveIngredient;

    public PackedScene IngredientDisplayScene = GD.Load<PackedScene>("uid://ckghx552h32nh");

    protected VariantData variantData;

    protected TreeItem root;

    protected string UnitOptions;

    protected bool editModeActive;
    protected EventBus eventBus;


    //TODO Funktionen zum EditMode aufräumen, schauen, was man in extra Funktion zusammenfassen kann
    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
    }


    public void Init(VariantData variantData, string recipeDescription)
    {
        this.variantData = variantData;
        RecipeDescription.Text = recipeDescription;
        VariantDescription.Text = variantData.variantDescription;
        root = IngredientList.CreateItem();

        foreach (IngredientData ingredient in variantData.ingredients)
        {
            CreateIngredient(ingredient);
        }
        foreach (string value in GlobalTypes.UnitText.Values)
        {
            UnitOptions += (value + ",");
        }
        editModeActive = false;
    }

    public void SetEditMode(bool editing)
    {
        editModeActive = editing;
        SetEditModeLayout(editing);
        VariantNameEdit.Text = variantData.variantName;
        foreach (var item in root.GetChildren())
        {
            item.SetText(0, item.GetMetadata(0).ToString());
            item.SetCellMode(1, TreeItem.TreeCellMode.Range);
            item.SetEditable(1, editing);
            item.SetText(1, UnitOptions);
            item.SetRange(1, (int)item.GetMetadata(1));
            item.SetSelectable(1, editing);
            item.SetEditable(2, editing);
            item.SetSelectable(2, editing);
            item.AddButton(2, RemoveIngredient);
        }
        VariantDescriptionEdit.Text = variantData.variantDescription;
    }

    protected void SetEditModeLayout(bool editing)
    {
        VariantNameEdit.Visible = editing;
        AddIngredientButton.Visible = editing;
        RecipeDescriptionLabel.Visible = !editing;
        RecipeDescription.Visible = !editing;
        VariantDescription.Visible = !editing;
        VariantDescriptionEdit.Visible = editing;
    }
    protected void OnAddIngredientButtonPressed()
    {
        var item = root.CreateChild();
        item.SetEditable(0, true);
        item.SetCellMode(1, TreeItem.TreeCellMode.Range);
        item.SetEditable(1, true);
        item.SetText(1, UnitOptions);
        item.SetSelectable(1, true);
        item.SetEditable(2, true);
        item.SetSelectable(2, true);
        item.AddButton(2, RemoveIngredient);
    }

    protected void OnDeleteIngredientButtonPressed(TreeItem item, int column, int id, int mouseButtonIndex)
    {
        item.Free();
    }

    protected void CreateIngredient(IngredientData ingredient)
    {
        var item = root.CreateChild();
        item.SetText(0, ingredient.baseQuantity.ToString());
        item.SetEditable(0, true);
        item.SetMetadata(0, ingredient.baseQuantity);
        item.SetTextAlignment(0, HorizontalAlignment.Right);
        item.SetText(1, GlobalTypes.UnitLabels[ingredient.unit]);
        item.SetSelectable(1, false);
        item.SetMetadata(1, (int)ingredient.unit);
        item.SetText(2, ingredient.ingredientName);
        item.SetSelectable(2, false);
    }

    public void CancelEditMode(bool editing)
    {
        editModeActive = editing;
        SetEditModeLayout(editing);
        foreach (var item in root.GetChildren())
        {
            item.Free();
        }
        foreach (IngredientData ingredient in variantData.ingredients)
        {
            CreateIngredient(ingredient);
        }
        editModeActive = editing;

    }

    public void ResetEditMode()
    {
        VariantNameEdit.Text = variantData.variantName;
        VariantDescriptionEdit.Text = variantData.variantDescription;
        foreach (var item in root.GetChildren())
        {
            item.Free();
        }
        foreach (var ingredient in variantData.ingredients)
        {
            var item = root.CreateChild();
            item.SetText(0, ingredient.baseQuantity.ToString());
            item.SetEditable(0, true);
            item.SetMetadata(0, ingredient.baseQuantity);
            item.SetCellMode(1, TreeItem.TreeCellMode.Range);
            item.SetEditable(1, true);
            item.SetText(1, UnitOptions);
            item.SetMetadata(1, (int)ingredient.unit);
            item.SetRange(1, (int)ingredient.unit);
            item.SetSelectable(1, true);
            item.SetText(2, ingredient.ingredientName);
            item.SetEditable(2, true);
            item.SetSelectable(2, true);
            item.AddButton(2, RemoveIngredient);
        }
    }

    protected void OnIngredientListItemSelected()
    {
        var item = IngredientList.GetSelected();
        if (IngredientList.GetSelectedColumn() == 0)
        {
            bool isFloat = item.GetText(0).IsValidFloat();
            if (editModeActive)
            {
                eventBus.EmitSignal("QuantityTextFieldEdited", isFloat);
                if (!isFloat)
                {
                    Color red = new Color(1, 0, 0, 1);
                    item.SetCustomBgColor(0, red);
                }
                else
                
                {
                    item.ClearCustomBgColor(0);
                }
            }
            else
            {
                CalculateQuantity(isFloat, item);
            }
        }
    }

    protected void CalculateQuantity(bool isFloat, TreeItem item)
    {
        if (!isFloat)
        {
            foreach (var ingredient in root.GetChildren())
            {
                ingredient.SetText(0, ingredient.GetMetadata(0).ToString());
            }
            return;
        }
        float factor = item.GetText(0).ToFloat() / (float)item.GetMetadata(0);
        foreach (var ingredient in root.GetChildren())
        {
            float newQuantity = (float)ingredient.GetMetadata(0) * factor;
            ingredient.SetText(0, newQuantity.ToString());
        }
    }


    public VariantData ApplyChanges()
    {
        variantData.variantName = VariantNameEdit.GetText();
        variantData.variantDescription = VariantDescriptionEdit.GetText();
        variantData.ingredients.Clear();
        foreach (var ingredient in root.GetChildren())
        {
            var index = ingredient.GetIndex();
            var ingredientData = new IngredientData();
            ingredientData.baseQuantity = ingredient.GetText(0).ToFloat();
            ingredientData.unit = (GlobalTypes.Unit)ingredient.GetRange(1);
            ingredientData.ingredientName = ingredient.GetText(2);
            variantData.ingredients.Add(ingredientData);
        }
        return variantData;

        //get variant name, description, etc. from edit
        //update variantData
        //return variantData
    }
}
