using Godot;
using System;

public partial class MovingPlatform : PathFollow2D
{
	[Export] private float _speed = 150.0f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Progress += _speed * (float)delta;
	}
}
