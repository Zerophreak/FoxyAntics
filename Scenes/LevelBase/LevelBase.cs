using Godot;
using System;

public partial class LevelBase : Node
{
	[Export] private PackedScene _bulletScene;

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("quit"))
		{
			GameManager.ChangeToMain();
		}
		if (@event.IsActionPressed("test"))
		{
			//SignalHub.EmitOnCreateBullet(new Vector2(150, -50), new Vector2(1, 1), 50.0f, _bulletScene);
			//_shooter.Shoot(new Vector2(1,1));
		}
	}
}
