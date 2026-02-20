using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance {get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
}
