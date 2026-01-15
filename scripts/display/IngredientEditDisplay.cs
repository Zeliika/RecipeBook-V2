using Godot;
using System;

public partial class IngredientEditDisplay : HBoxContainer
{
    [Export] LineEdit QuantityEdit;
    [Export] OptionButton UnitDropdown;
    [Export] LineEdit IngredientNameEdit;
    [Export] Button DeleteIngredientButton;
    protected EventBus eventBus;

    public override void _Ready()
    {
        foreach (GlobalTypes.Unit unit in GlobalTypes.UnitLabels.Keys)
        {
            UnitDropdown.AddItem(GlobalTypes.UnitLabels[unit]);
        }
        eventBus = GetNode<EventBus>("/root/EventBus");

    }

    public void Init(IngredientData ingredientData = null)
    {
        if (ingredientData == null)
        {
            return;
        }
        QuantityEdit.Text = ingredientData.baseQuantity.ToString();
        UnitDropdown.Select((int)ingredientData.unit);
        IngredientNameEdit.Text = ingredientData.ingredientName;
    }

    public IngredientData CreateIngredientData()
    {
        var ingredientData = new IngredientData();
        ingredientData.baseQuantity = QuantityEdit.Text.ToFloat();
        ingredientData.unit = (GlobalTypes.Unit)UnitDropdown.GetSelectedId();
        ingredientData.ingredientName = IngredientNameEdit.Text;
        return ingredientData;
    }

    protected void OnDeleteIngredientButtonPressed()
    {
        eventBus.EmitSignal(EventBus.SignalName.IngredientDeleted, GetIndex());
    }

}
