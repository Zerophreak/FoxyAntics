using Godot;
using System;

public partial class Checkpoint : Area2D
{
	const string TRIGGER_PATH = "parameters/conditions/on_trigger";

	[Export] private AnimationTree _animationTree;
	[Export] private AudioStreamPlayer2D _sound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		SignalManager.Instance.OnBossKilled += OnBossKilled;
	}

    public override void _ExitTree()
    {
        SignalManager.Instance.OnBossKilled -= OnBossKilled;
    }


    private void OnBossKilled(int points)
    {
        SetDeferred(PropertyName.Monitoring, true);
		_animationTree.Set(TRIGGER_PATH, true);
    }


    private void OnAreaEntered(Area2D area)
    {
        SignalManager.EmitOnLevelComplete();
		SoundManager.PlayClip(_sound, SoundManager.SOUND_WIN);
    }

}
