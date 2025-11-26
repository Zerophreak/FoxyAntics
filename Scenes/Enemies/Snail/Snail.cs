using Godot;
using System;

public partial class Snail : EnemyBase
{
	[Export] private RayCast2D _floorDetection;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		Vector2 velocity = Velocity;

		if(!IsOnFloor())
        {
            velocity.Y += _gravity * (float)delta;
        }
		else
        {
            velocity.X = _animatedSprite2D.FlipH ? _speed : -_speed;
        }

		Velocity = velocity;

		MoveAndSlide();
		FlipMe();
    }

	private void FlipMe()
    {
        if(IsOnFloor() && ( IsOnWall() || !_floorDetection.IsColliding() ))
        {
            _animatedSprite2D.FlipH = !_animatedSprite2D.FlipH;
			_floorDetection.Position = new Vector2(
				_floorDetection.Position.X * -1,
				_floorDetection.Position.Y
			);
        }
    }
}
