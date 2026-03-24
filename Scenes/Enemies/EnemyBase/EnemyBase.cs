using Godot;
using System;
using System.Security;
using System.Threading.Tasks.Dataflow;

public partial class EnemyBase : CharacterBody2D
{
	[Export] private VisibleOnScreenNotifier2D _screenNotifier;
	[Export] protected AnimatedSprite2D _animatedSprite2D;
	[Export] private HitBox _hitBox;
	
	[Export] protected float _speed = 30.0f;
	[Export] protected float _fallenOffY = 200.0f;

	protected float _gravity = 800.0f;
	protected Player _playerRef; 


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerRef = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_PLAYER) as Player;
		if(_playerRef == null)
		{
			GD.PrintErr("No Player ref");
			QueueFree();
		}
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
