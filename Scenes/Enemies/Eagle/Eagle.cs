using Godot;
using System;

public partial class Eagle : EnemyBase
{
	[Export] private Shooter _shooter;
	[Export] private RayCast2D _playerDetect;

	private readonly Vector2 FLY_SPEED = new Vector2(35.0f, 15.0f);
	private Vector2 _flyDirection = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        Velocity = _flyDirection;
		MoveAndSlide();
		Shoot();
    }
	private void Shoot()
	{
		if(_playerDetect.IsColliding())
		{
			_shooter.Shoot(GlobalPosition.DirectionTo(_playerDetect.GlobalPosition));
		}
	}
	protected override void OnScreenEntered()
    {
        base.OnScreenEntered();
		_animatedSprite2D.Play("eagle");
		FlyToPlayer();
    }
	protected override void OnTimeout()
	{
		FlyToPlayer();
	}

	private void FlyToPlayer()
	{
		FlipMe();
		float xDir = _animatedSprite2D.FlipH ? 1f : -1f;
		_flyDirection = new Vector2(FLY_SPEED.X * xDir, FLY_SPEED.Y);
	}
}