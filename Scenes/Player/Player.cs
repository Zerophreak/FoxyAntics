using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private const float GRAVITY = 690.0f;
	private const float RUN_SPEED = 120.0f;
	private const float JUMP_SPEED = -270.0f;
	private const float MAX_FALL = 350.0f;

	[Export]private Label _debugLabel;
	[Export] private AudioStreamPlayer2D _jumpSound;

	private bool _jumped = false;

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("jump"))
		{
			_jumped = true;
		}
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
		velocity.X = Input.GetAxis("left", "right") * RUN_SPEED;

		if(IsOnFloor() && _jumped)
		{
			velocity.Y = JUMP_SPEED;
			_jumped = false;
			_jumpSound.Play();
		}

		return velocity;

	}
}
