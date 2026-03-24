using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	[Export] private VisibleOnScreenNotifier2D _screenNotifier;
	[Export] protected AnimatedSprite2D _animatedSprite2D;
	[Export] private HitBox _hitBox;
	
	[Export] protected float _speed = 30.0f;
	[Export] protected float _fallenOffY = 200.0f;

	protected float _gravity = 800.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		FallenOff();
	}

	private void FallenOff()
	{
		if(GlobalPosition.Y > _fallenOffY)
		{
			CallDeferred(MethodName.QueueFree);
		}
	}

	protected Vector2 ApplyGravity(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += _gravity * (float)delta;
		return velocity;
	}
}
