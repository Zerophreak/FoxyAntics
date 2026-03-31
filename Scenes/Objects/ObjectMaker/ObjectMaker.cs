using Godot;
using System;
using System.IO.Pipes;

public partial class ObjectMaker : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		SignalHub.Instance.OnCreateBullet += OnCreateBullet;
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
}
