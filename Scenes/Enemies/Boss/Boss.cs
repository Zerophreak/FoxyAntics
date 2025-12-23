using Godot;
using System;
using System.ComponentModel;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger; 
	[Export] private Area2D _hitBox;

	private bool _invincible = false;
	private AnimationNodeStateMachinePlayback _stateMachine;


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

	private void TakeDamage()
	{
		if(_invincible) return;

		SetInvincible(true);
		_stateMachine.Travel("hit");
	} 
	private void OnHitBoxAreaEntered(Area2D area)
	{
		TakeDamage();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
