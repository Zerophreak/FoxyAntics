using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private readonly Vector2 HURT_JUMP_VELOCITY = new Vector2(0, -130f);

	private const float GRAVITY = 690.0f;
	private const float RUN_SPEED = 120.0f;
	private const float JUMP_SPEED = -270.0f;
	private const float MAX_FALL = 350.0f;

	public bool IsStill {get {return Mathf.IsZeroApprox(Velocity.X); }}
	public bool IsFalling { get { return Velocity.Y > 0; }}
	public bool OnFloor { get { return IsOnFloor(); }}
	public bool IsHurt {get {return _isHurt; }}

	[Export]private Label _debugLabel;
	[Export] private AudioStreamPlayer2D _jumpSound;
	[Export] private AudioStreamPlayer2D _hurtSound;
	[Export] private Sprite2D _sprite;
	[Export] private Shooter _shooter;
	[Export] private Timer _hurtTimer;
	[Export] private HitBox _hitBox;


	private bool _jumped = false;
	private bool _isHurt = false;

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("jump"))
		{
			_jumped = true;
		}

		 if(@event.IsActionPressed("shoot"))
		{
			Vector2 direction = _sprite.FlipH ? Vector2.Left : Vector2.Right;
			_shooter.Shoot(direction);
		}
    }

	public override void _EnterTree()
	{
		AddToGroup(GameConstants.GROUP_PLAYER);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hitBox.AreaEntered += OnHitboxAreaEntered;
		_hurtTimer.Timeout += OnHurtTimerTimeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += GRAVITY * (float)delta;
		velocity = GetInput(velocity);
		
		velocity.Y = Mathf.Clamp(velocity.Y, JUMP_SPEED, MAX_FALL);

		Velocity = velocity;

		MoveAndSlide();

		_debugLabel.Text = velocity.Y.ToString("F2");
	}

	private Vector2 GetInput(Vector2 velocity)
	{
		if(IsHurt) return velocity;

		velocity.X = Input.GetAxis("left", "right") * RUN_SPEED;

		if(IsOnFloor() && _jumped)
		{
			velocity.Y = JUMP_SPEED;
			_jumped = false;
			_jumpSound.Play();
		}

		if(!Mathf.IsZeroApprox(velocity.X))
		{
			_sprite.FlipH = velocity.X < 0;
		}

		return velocity;
	}

	private void ApplyHurtJump()
	{
		_isHurt = true;
		_hurtTimer.Start();
		_hurtSound.Play();
		Velocity = HURT_JUMP_VELOCITY;
	}

	//signal functions
	private void OnHitboxAreaEntered(Area2D area)
	{
		CallDeferred(nameof(ApplyHurtJump));
	}

	private void OnHurtTimerTimeout()
	{
		_isHurt = false;
	}
}
