using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	public const string GroupName = "Player";
	
	private enum PlayerState { Idle, Run, Jump, Fall, Hurt } 

	private const float GRAVITY = 690.0f;
	private const float JUMP_VELOCITY = -260.0f;
	private const float MAX_FALL = 400.0f;
	private const float RUN_SPEED = 120.0f;

	[Export] private float _yFallOff = 100.0f;
	[Export] private Sprite2D _sprite2D;
	[Export] private AudioStreamPlayer2D _sound;
	[Export] private AnimationPlayer _animationPlayer;
	[Export] private Label _debugLabel;
	
	private PlayerState _state = PlayerState.Idle; 

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{	
		Velocity = GetInput((float)delta);
		MoveAndSlide();
		CalculateStates();
		UpdateDebugLabel();
	}

	private void UpdateDebugLabel()
    {
        string s = "";
		s += $"floor: {IsOnFloor()}\n";
		s += $"{_state}\n";
		s += $"{Velocity.X:0f}, {Velocity.Y:0f}";
		_debugLabel.Text = s; 
    }

	private void FallenOff()
    {
        if(GlobalPosition.Y > _yFallOff)
        {
           SetPhysicsProcess(false); 
        }
    }

    private Vector2 GetInput(float delta)
    {
        Vector2 newVelocity = Velocity;
		newVelocity.X = 0;
		newVelocity.Y += GRAVITY * delta;

		if(Input.IsActionPressed("left"))
        {
            newVelocity.X = -RUN_SPEED;
			_sprite2D.FlipH = true;
        }

		if (Input.IsActionPressed("right"))
		{
			newVelocity.X = RUN_SPEED;
			_sprite2D.FlipH = false;
		}

		if(IsOnFloor() && Input.IsActionPressed("jump"))
        {
            newVelocity.Y = JUMP_VELOCITY;
			SoundManager.PlayClip(_sound,SoundManager.SOUND_JUMP);
        }

		newVelocity.Y = Mathf.Clamp(newVelocity.Y, JUMP_VELOCITY, MAX_FALL);

		return newVelocity;
    }

	private void CalculateStates()
    {
		PlayerState newState;

		if (IsOnFloor())
        {
            if (Velocity.X == 0)
            
                newState = PlayerState.Idle;
			else
				newState = PlayerState.Run; 
        }
		else
        {
            if (Velocity.Y > 0 )
				newState = PlayerState.Fall;
			else
				newState = PlayerState.Jump;
        }

		SetState(newState); 
    }

	private void SetState(PlayerState newState)
    {
        if(newState == _state) return;

		_state = newState;

		// Idle, Run, Jump, Fall. 
		switch (_state)
        {
            case PlayerState.Idle:
				_animationPlayer.Play("idle");
				break;
			case PlayerState.Run:
				_animationPlayer.Play("run");
				break;
			case PlayerState.Jump:
				_animationPlayer.Play("jump");
				break;
			case PlayerState.Fall:
				_animationPlayer.Play("fall");
				break;
        }
    }

}
