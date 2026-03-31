using Godot;
using System;

public partial class BulletBase : Area2D
{
	

	private Vector2 _direction = Vector2.Right;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition += _direction * (float)delta;
	}

	public void Setup(Vector2 pos, Vector2 dir, float speed)
	{
		GlobalPosition = pos;
		_direction = dir * speed;
	}
}
