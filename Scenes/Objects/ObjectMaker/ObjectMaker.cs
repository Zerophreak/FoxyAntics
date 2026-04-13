using Godot;
using System;
using System.IO.Pipes;

public partial class ObjectMaker : Node
{
    [Export] private PackedScene _explosionScene;
    public override void _Ready()
    {
		SignalHub.Instance.OnCreateBullet += OnCreateBullet;
		SignalHub.Instance.OnCreateExplosion += OnCreateExplosion;
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

}
