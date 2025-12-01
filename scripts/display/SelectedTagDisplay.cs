using Godot;
using System;

public partial class SelectedTagDisplay : PanelContainer
{
    [Export] public Label SelectedTagName;
    [Export] public Button RemoveTagButton;

    [Signal]
    public delegate void OnTagRemovedEventHandler(int id);

    public void Init(string TagName)
    {
        SelectedTagName.Text = TagName;
    }

    public void RemoveSelectedTag()
    {
        EmitSignal(nameof(OnTagRemoved), GetIndex()); 
        Visible = false;
    }
}
