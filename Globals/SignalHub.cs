using Godot;
using System;
using System.ComponentModel;

public partial class SignalHub : Node
{
	public static SignalHub Instance {get; private set; }

	[Signal] public delegate void OnCreateBulletEventHandler(Vector2 pos, Vector2 dir, float speed, PackedScene scene);
	[Signal] public delegate void OnCreateExplosionEventHandler(Vector2 pos);
	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnCreateBullet(Vector2 pos, Vector2 dir, float speed, PackedScene scene)
	{
		Instance.EmitSignal(SignalName.OnCreateBullet, pos, dir, speed, scene);
	}

	public static void EmitOnExplosion(Vector2 pos)
	{
		Instance.EmitSignal(SignalName.OnCreateExplosion, pos);
	}
}
