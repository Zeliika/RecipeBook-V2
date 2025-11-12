using Godot;
using System;

public partial class IngredientDisplay : HBoxContainer
{
    [Export] protected LineEdit QuantityTextField;

    [Export] protected Label UnitLabel;

    [Export] protected Label IngredientLabel;

    [Export] protected PanelContainer Placeholder;

    public override void _Ready()
    {
        
    }
    public void Init(string ingredient_name = "", double base_quantity = 0.0, GlobalTypes.Unit unit = GlobalTypes.Unit.NONE)
    {
        QuantityTextField.Text = base_quantity.ToString();
        QuantityTextField.Visible = base_quantity > 0;
        Placeholder.Visible = base_quantity <= 0;
        UnitLabel.Text = GlobalTypes.UnitLabels[unit];
        IngredientLabel.Text = ingredient_name;
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
