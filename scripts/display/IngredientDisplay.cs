using Godot;
using System;

public partial class IngredientDisplay : HBoxContainer
{
    [Export] public LineEdit QuantityTextField;

    [Export] public Label UnitLabel;

    [Export] public Label IngredientLabel;

    [Export] public PanelContainer Placeholder;

    public override void _Ready()
    {
        
    }
    public void Init(IngredientData ingredientData)
    {
        QuantityTextField.Text = ingredientData.baseQuantity.ToString();
        QuantityTextField.Visible = ingredientData.baseQuantity > 0;
        Placeholder.Visible = ingredientData.baseQuantity <= 0;
        UnitLabel.Text = GlobalTypes.UnitLabels[ingredientData.unit];
        IngredientLabel.Text = ingredientData.ingredientName;
    }


    public void SetQuantity(float NewQuantity)
    {
        QuantityTextField.Text = NewQuantity.ToString();
    }

    public LineEdit GetQuantityTextField()
    {
        return QuantityTextField;
    }
}
