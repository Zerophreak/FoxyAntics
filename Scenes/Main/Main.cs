using Godot;
using System;

public partial class Main : Control
{
    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("quit"))
		{
			GameManager.ChangeToLevel();
		}
    }

}
