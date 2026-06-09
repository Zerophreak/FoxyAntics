using Godot;
using System;

public partial class Main : Control
{

    public override void _Ready()
    {
        GetTree().Paused = false;
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("shoot"))
        {
            GameManager.ChangeToLevel();
        }
        if (@event.IsActionPressed("quit"))
		{
			GetTree().Quit();
		}
    }

}
