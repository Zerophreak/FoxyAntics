using Godot;
using System;

public partial class Eagle : EnemyBase
{
	[Export] private Timer _directionTimer;

	private readonly Vector2 FLY_SPEED = new Vector2(35, 15);
	private Vector2 _flyDirection = new Vector2();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_directionTimer.Timeout += OndirectionTimerTimeout;
	}


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Velocity = _flyDirection;
		MoveAndSlide();
	}

	private void SetDirectionAndFlip()
    {
        int xDirection = Math.Sign(_playerRef.GlobalPosition.X - GlobalPosition.X);
		_animatedSprite2D.FlipH = xDirection > 0;
		_flyDirection = new Vector2(xDirection, 1) * FLY_SPEED;
    }

	private void FlyToPLayer()
    {
		SetDirectionAndFlip();
        _directionTimer.Start();
    }

	private void OndirectionTimerTimeout()
	{
		FlyToPLayer();
	}

    protected override void OnScreenEntered()
    {
		_animatedSprite2D.Play("fly");
		FlyToPLayer();
	
    }

}
