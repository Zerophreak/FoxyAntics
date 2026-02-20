using Godot;
using System;

public partial class LevelBase : Node
{
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("quit"))
		{
			GameManager.ChangeToMain();
		}
	}
}
