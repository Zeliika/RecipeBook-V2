using Godot;
using System;

public partial class TagDisplay : Control
{
    [Export] public Label TagNameLabel;

    public void Init(string tag_name)
    {
        TagNameLabel.Text = tag_name;
    }


}
