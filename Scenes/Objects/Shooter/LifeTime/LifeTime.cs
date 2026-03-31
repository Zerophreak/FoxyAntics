using Godot;
using System;

public partial class LifeTime : Node
{
	[Export] private Timer _timer;
	[Export] private float _waitTime;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer.Start(_waitTime);
		_timer.Timeout += OnTimeout;
	}

    private void OnTimeout()
    {
        GetParent().QueueFree();
    }

    
}
