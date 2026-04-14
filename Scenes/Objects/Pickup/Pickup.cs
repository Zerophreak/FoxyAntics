using Godot;
using System;

public partial class Pickup : Area2D
{
	[Export] private AnimatedSprite2D _animatedSprite2D;
	[Export] private int _points = 2;

	public override void _Ready()
	{
		PlayRandomAnimation();
	}

	private void PlayRandomAnimation()
	{
		var animNames = _animatedSprite2D.SpriteFrames.GetAnimationNames();

		if(animNames.Length > 0)
		{
			string randName = animNames[new Random().Next(animNames.Length)];
			_animatedSprite2D.Play(randName);
		}

	}
}
