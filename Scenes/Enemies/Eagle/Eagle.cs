using Godot;
using System;

public partial class Eagle : EnemyBase
{
    protected override void OnScreenEntered()
    {
        base.OnScreenEntered();
		_animatedSprite2D.Play("eagle");
    }
}