using Godot;
using System;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger;
	
	public override void _Ready()
	{
		_trigger.AreaEntered += OntriggerAreaEntered;
	}

	public void Shoot()
	{
		GD.Print("Boss Shoot");
	}

    private void OntriggerAreaEntered(Area2D area)
    {	
		//condition
        _animationTree.Set("parameters/conditions/on_trigger", true);
		_trigger.AreaEntered -= OntriggerAreaEntered;
    }

}
