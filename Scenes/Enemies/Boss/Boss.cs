// <{oUtCaSt-LaB gAmEs}>
using Godot;
using System;
using System.ComponentModel;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger;
	[Export] private Shooter _Shooter;
	[Export] private Node2D _visuals;
	[Export] private HitBox _hitBox;
	[Export] private int _lives = 3;
	[Export] private int _points = 20;

	private Player _playerRef;
	private AnimationNodeStateMachinePlayback _state;
	private Vector2 _visualsPosition;
	private bool _invincible = false;

	public override void _Ready()
	{
		_playerRef = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_PLAYER) as Player;
		if (_playerRef == null)
		{
			GD.PrintErr("No Player ref");
			QueueFree();
		}
		_visualsPosition = _visuals.Position;

		_trigger.AreaEntered += OntriggerAreaEntered;
		_hitBox.AreaEntered += OnHitBoxAreaEntered;
		_animationTree.AnimationFinished += OnAnimationFinished;
		
		_state = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
	}

    public void ActivateHitBox()
	{
		_hitBox.Activate(true);
	}
	public void Shoot()
	{
		GD.Print("Boss Shoot");
		_Shooter.Shoot(_visuals.GlobalPosition.DirectionTo(_playerRef.GlobalPosition));
	}

	private void TweenHit()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(_visuals, Node2D.PropertyName.Position.ToString(), _visualsPosition, 1.8f);
	}

	private void ReduceLives()
	{
		_lives--; 
		if(_lives <= 0)
		{
			Die();
		}
	}

    private void Die()
    {
		SignalHub.EmitOnBossKilled();
		SignalHub.EmitOnScored(_points);
        QueueFree();
    }

	private void TakeDamage()
	{
		if(_invincible) return;
		_invincible = true;
		_state.Travel("hit");
		TweenHit();
		ReduceLives();
	}

	// signal methods
	private void OntriggerAreaEntered(Area2D area)
    {	
		//condition
        _animationTree.Set("parameters/conditions/on_trigger", true);
		_trigger.AreaEntered -= OntriggerAreaEntered;
    }
	
	private void OnHitBoxAreaEntered(Area2D area)
	{
		TakeDamage();
	}

	private void OnAnimationFinished(StringName animName)
	{
		if(animName == "hit")
		{
			_invincible = false;
		}
	}
}
