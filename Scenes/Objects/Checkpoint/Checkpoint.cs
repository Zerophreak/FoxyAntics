using Godot;
using System;

public partial class Checkpoint : Area2D
{
	[Export] private AnimationTree _animationTree;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnBossKilled += OnBossKilled;
		_animationTree.AnimationFinished += OnAnimationFinished;
		AreaEntered += OnAreaEntered;
	}

    public override void _ExitTree()
	{
		SignalHub.Instance.OnBossKilled -= OnBossKilled;
	}

    // signal methods 
    private void OnBossKilled()
	{
		_animationTree.Set("parameters/conditions/boss_killed", true);
	}

	private void OnAnimationFinished(StringName animName)
	{
		if(animName == "open")
		{
			SetDeferred(Area2D.PropertyName.Monitoring, true);
		}
	}

	private void OnAreaEntered(Area2D area)
	{
		SignalHub.EmitOnLevelComplete();
		AreaEntered -= OnAreaEntered;
	}
}
