using Godot;
using System;
using System.Collections;
using System.Runtime.InteropServices.Marshalling;

public partial class EnemyBase : CharacterBody2D
{
	[Export] protected int _points = 1;
	[Export] protected float _speed = 30.0f;
	[Export] protected float _yFallOff = 100.0f;
	
	[Export] protected VisibleOnScreenNotifier2D _visibleOnScreenNotifier2D;
	[Export] protected AnimatedSprite2D _animatedSprite2D;
	[Export] protected Area2D _hitBox;
	
	protected Player _playerRef;
	protected float _gravity = 800.0f;

	public override void _Ready()
    {
		_playerRef = (Player)GetTree().GetFirstNodeInGroup(Player.GroupName);
		_visibleOnScreenNotifier2D.ScreenEntered += OnScreenEntered;
		_visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
		_hitBox.AreaEntered += OnHitbocAreaEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		FallenOff();
	}

	private void FallenOff()
    {
        if(GlobalPosition.Y > _yFallOff)
        {
            QueueFree();
        }
    }

    private void OnHitbocAreaEntered(Area2D area)
    {
        Die();
    }

    private void Die()
    {
		SignalManager.EmitOnEnemyHit(_points, GlobalPosition);
		SignalManager.EmitOnCreateObject(GlobalPosition, (int)GameObjectType.Explosion);
		SignalManager.EmitOnCreateObject(GlobalPosition, (int)GameObjectType.Pickup);
		SetPhysicsProcess(false);
		QueueFree();
    }


    protected virtual void OnScreenExited()
    {

    }

    protected virtual void OnScreenEntered()
    {
    }
}
