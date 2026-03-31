using Godot;
using System;

public partial class Eagle : EnemyBase
{

	private readonly Vector2 FLY_SPEED = new Vector2(35.0f, 15.0f);
    public override void _PhysicsProcess(double delta)
    {
        Velocity = _flyDirection;
		MoveAndSlide();
    }

	private Vector2 _flyDirection = Vector2.Zero;
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