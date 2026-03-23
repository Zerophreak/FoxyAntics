using Godot;
using System;


public partial class Snail : EnemyBase
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += _gravity * (float)delta;
		Velocity = velocity;
		MoveAndSlide();
	}
}
