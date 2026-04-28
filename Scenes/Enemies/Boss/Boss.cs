using Godot;
using System;
using System.ComponentModel;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger;
	[Export] private Shooter _Shooter;
	[Export] private Node2D _visuals;

	private Player _playerRef;

	public override void _Ready()
	{
		_trigger.AreaEntered += OntriggerAreaEntered;

		_playerRef = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_PLAYER) as Player;
		if (_playerRef == null)
		{
			GD.PrintErr("No Player ref");
			QueueFree();
		}
	}

	public void Shoot()
	{
		GD.Print("Boss Shoot");
		_Shooter.Shoot(_visuals.GlobalPosition.DirectionTo(_playerRef.GlobalPosition));
	}

    private void OntriggerAreaEntered(Area2D area)
    {	
		//condition
        _animationTree.Set("parameters/conditions/on_trigger", true);
		_trigger.AreaEntered -= OntriggerAreaEntered;
    }
}
