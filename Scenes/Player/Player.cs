using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	private const float GRAVITY = 690.0f;
	private const float JUMP_VELOCITY = -260.0f;
	private const float MAX_FALL = 400.0f;
	private const float RUN_SPEED = 120.0f;

	[Export] private Sprite2D _sprite2D;
	[Export] private AudioStreamPlayer2D _sound;

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{	
		Velocity = GetInput((float)delta);
		MoveAndSlide();
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

}
