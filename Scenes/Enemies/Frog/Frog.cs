using Godot;
using System;

public partial class Frog : EnemyBase 
{
	private readonly Vector2 JUMP_VEL_R = new Vector2(100.0f, -150.0f);
	private readonly Vector2 JUMP_VEL_L = new Vector2(-100.0f, -150.0f);
	private bool _jump = false;

    public override void _Ready()
    {
        base._Ready();
		_timer.OneShot = true;
		_timer.WaitTime = GD.RandRange(2.0, 4.0);

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = ApplyGravity(delta);
		Velocity = velocity;
		ApplyJump();
		MoveAndSlide();
		FlipMe();

		if(IsOnFloor())
		{
			_animatedSprite2D.Play("frog_idle");
			Velocity = Vector2.Zero;
		}
	}

	private void ApplyJump()
	{
		if(IsOnFloor() && _jump)
		{	
			_animatedSprite2D.Play("frog_jump");
			Velocity = _animatedSprite2D.FlipH ? JUMP_VEL_R : JUMP_VEL_L;
			_jump = false;
			_timer.Start(GD.RandRange(2.0, 4.0));
		}
	}

	protected override void OnTimeout()
	{
		_jump = true;
		GD.Print("OnTimeout: ", _jump);
	}
}
