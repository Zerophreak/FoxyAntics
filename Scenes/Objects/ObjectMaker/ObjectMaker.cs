using Godot;
using System;
using System.IO.Pipes;

public partial class ObjectMaker : Node
{
    [Export] private PackedScene _explosionScene;
	[Export] private PackedScene _pickupScene;

    public override void _Ready()
    {
		SignalHub.Instance.OnCreateBullet += OnCreateBullet;
		SignalHub.Instance.OnCreateExplosion += OnCreateExplosion;
		SignalHub.Instance.OnCreatePickup += OnCreatePickup;
	}

    private void AddObject(Node node)
	{
		AddChild(node);
	}

    private void OnCreateBullet(Vector2 pos, Vector2 dir, float speed, PackedScene scene)
    {
		BulletBase bullet = scene.Instantiate<BulletBase>();
		bullet.Setup(pos, dir, speed);
		CallDeferred(MethodName.AddObject, bullet);
	}

	private void OnCreateExplosion(Vector2 pos)
	{
		Explosion explosion = _explosionScene.Instantiate<Explosion>();
		explosion.GlobalPosition = pos;
		CallDeferred(MethodName.AddObject, explosion);
	}

	private void OnCreatePickup(Vector2 pos)
	{
		Pickup pickup = _pickupScene.Instantiate<Pickup>();
		pickup.GlobalPosition = pos;
		CallDeferred(MethodName.AddObject, pickup);
	}

}
