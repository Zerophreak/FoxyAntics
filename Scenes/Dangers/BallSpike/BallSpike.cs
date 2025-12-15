using Godot;
using System;

public partial class BallSpike : PathFollow2D
{
	[Export] private float _speed = 50.0f;
	[Export] private float _rotationSpeed = 400.0f;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += _speed * (float)delta;
		RotationDegrees += _rotationSpeed * (float)delta;
	}
}
