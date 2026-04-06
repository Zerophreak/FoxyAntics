using Godot;
using System;

public partial class Shooter : Node2D
{
	[Export] private PackedScene _bulletScene;
	[Export] private AudioStreamPlayer2D _sound;
	[Export] private Timer _shootTimer;
	[Export] private float _speed = 50.0f;
	[Export] private float _shootDelay = 0.7f;

	private bool _canShoot = true;


	public override void _Ready()
	{
		_shootTimer.Timeout += OnShootTimerTimeout;
	}

	private void OnShootTimerTimeout()
	{
	}

	public void Shoot(Vector2 direction)
	{
		if(!_canShoot) return;
		_canShoot = false;

		SignalHub.EmitOnCreateBullet(GlobalPosition);
	}
}
