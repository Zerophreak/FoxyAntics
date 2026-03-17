using Godot;
using System;
using System.ComponentModel;

public partial class Hitbox : Area2D
{ 
	[Export] private Shape2D _shape;
	[Export] private CollisionShape2D _collisionShape2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_collisionShape2D.Shape = _shape;
	}
}
