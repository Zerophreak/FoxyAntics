using Godot;
using System;
using System.Security.AccessControl;

public partial class Frog : EnemyBase
{
	[Export] private Timer _jumpTimer;

	private const float JUMP_MIN_TIME = 2.0f;
	private const float JUMP_MAX_TIME = 3.0f;
	private const float JUMP_VELOCITY_X = 100f;
	private const float JUMP_VELOCITY_Y = 100f;

	private static readonly Vector2 JUMP_VELOCITY_R = new Vector2(JUMP_VELOCITY_X, -JUMP_VELOCITY_Y);
	private static readonly Vector2 JUMP_VELOCITY_L = new Vector2(-JUMP_VELOCITY_X, -JUMP_VELOCITY_Y); 

	private bool _seenPlayer = false;
	private bool _jump = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_jumpTimer.Timeout += OnjumpTimerTimeOut;
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		if(!IsOnFloor())
        {
            Velocity += new Vector2(0, _gravity * (float)delta);
        }
		else
        {
            Velocity = new Vector2(0, 0);
			_animatedSprite2D.Play("idle");
        }

		ApplyJump();
		MoveAndSlide();
		FlipMe();
    }

	private void FlipMe()
	{
		_animatedSprite2D.FlipH = _playerRef.GlobalPosition.X > GlobalPosition.X;
	}

	private void ApplyJump()
	{
		if(IsOnFloor() && _seenPlayer && _jump)
        {
            _jump = false;
			_animatedSprite2D.Play("jump");
			Velocity = _animatedSprite2D.FlipH ? JUMP_VELOCITY_R : JUMP_VELOCITY_L;
			StartTimer();
        }
	}

	private void StartTimer()
    {
		_jumpTimer.WaitTime = GD.RandRange(JUMP_MIN_TIME, JUMP_MAX_TIME);
		_jumpTimer.Start();
		GD.Print("Timer started with ", _jumpTimer.WaitTime);
    }

	private void OnjumpTimerTimeOut()
	{
		GD.Print("Timer TimeOut");
		_jump = true;
	}

	protected override void OnScreenEntered()
    {	
		if(_seenPlayer == false)
        {
			_seenPlayer = true;
			GD.Print("Seen Player:");
			StartTimer();
		}
    }

	
}
