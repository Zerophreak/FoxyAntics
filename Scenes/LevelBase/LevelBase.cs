using Godot;
using System;

public partial class LevelBase : Node2D
{

	public override void _Process(double delta)
	{
		//For testing
		if(Input.IsActionJustPressed("schoot"))
        {
            SignalManager.EmitOnCreateBullet(
				new Vector2(100, -100),
				Vector2.Right,
				100.0f,
				2.0f,
				(int)GameObjectType.BulletPlayer
			);
        }
	}
}
