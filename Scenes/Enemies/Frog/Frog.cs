using Godot;
using System;

public partial class Frog : EnemyBase 
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = ApplyGravity(delta);
		Velocity = velocity;
		MoveAndSlide();
		FlipMe();
	}
}
