using Godot;
using System;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_trigger.AreaEntered += OntriggerAreaEntered;
	}

    private void OntriggerAreaEntered(Area2D area)
    {
        _animationTree.Set("parameters/conditions/on_trigger", true);
		_trigger.AreaEntered -= OntriggerAreaEntered;
    }

}
