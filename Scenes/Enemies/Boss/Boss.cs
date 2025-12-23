using Godot;
using System;
using System.ComponentModel;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	//TODO trigger doesnt play the arrive is on auto
	[Export] private Area2D _trigger; 
	[Export] private Area2D _hitBox;
	[Export] private Node2D _visual;
	[Export] private int _lives = 2;
	[Export] private int _points = 5;


	private bool _invincible = false;
	private AnimationNodeStateMachinePlayback _stateMachine;
	private Tween _hitTween;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_trigger.AreaEntered += OntriggerAreaEntered;
		_hitBox.AreaEntered += OnHitBoxAreaEntered;
		_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");

	}

    private void OntriggerAreaEntered(Area2D area)
    {
        _animationTree.Set("parameters/conditions/on_trigger", true);
		_hitBox.SetDeferred(Area2D.PropertyName.Monitoring, true);
    }

	private void SetInvincible(bool value)
	{
		_invincible = value;
	}

	private void ReduceLives()
	{
		_lives--;
		if(_lives <= 0 )
			Die();

	}

	private void Die()
	{
		if(_hitTween != null)
		{
			_hitTween.Kill();
		}
		
		SignalManager.EmitOnBossKilled(_points);
		QueueFree();
	}
	private void TakeDamage()
	{
		if(_invincible) return;

		SetInvincible(true);
		_stateMachine.Travel("hit");
		TweenHit();
		ReduceLives();
	} 
	private void OnHitBoxAreaEntered(Area2D area)
	{
		TakeDamage();
		GD.Print("Boss OnHitBoxAreaEntered");
	}

	private void TweenHit()
	{
		_hitTween = GetTree().CreateTween();
		_hitTween.TweenProperty(_visual, Node2D.PropertyName.Position.ToString(), Vector2.Zero, 1.6f);
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
