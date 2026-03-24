using Godot;
using System;

public partial class Frog : EnemyBase 
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = ApplyGravity(delta);
		Velocity = velocity;
		MoveAndSlide();
		FlipMe();
	}
}
