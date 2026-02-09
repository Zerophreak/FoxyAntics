using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

class TargetDistanceTime
{
	public Vector2 PositionTo;
	public float Time;

	public TargetDistanceTime(Vector2 positionFrom, Vector2 positionTo, float speed)
	{
		Time = positionFrom.DistanceTo(positionTo)/ speed;
		PositionTo = positionTo;
	}

    public override string ToString()
    {
        return $"PositionTo: {PositionTo}, Time: {Time}";
    }
}
public partial class MovingPlatformAnimate : AnimatableBody2D
{
	[Export] private Godot.Collections.Array<Marker2D> _points = new();
	[Export] private float _speed = 150.0f;

	private List<TargetDistanceTime> _targetPoints = new();
	private Tween _tween;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (_points.Count < 2) return;

		GlobalPosition = _points[0].GlobalPosition;
		SetupPoints();
	}

	private void SetupPoints()
	{
		for (int i = 0; i < _points.Count - 1; i++)
		{
			_targetPoints.Add(new TargetDistanceTime(_points[i].GlobalPosition, _points[i + 1].GlobalPosition, _speed));
		}
		_targetPoints.Add(new TargetDistanceTime(_points[_points.Count - 1].GlobalPosition, _points[0].GlobalPosition, _speed));

		foreach( var item in _targetPoints)
		{
			GD.Print(item);
		}
	}
	
    public override void _ExitTree()
    {
        if(_tween != null)
		{
			_tween.Kill();
		}
    }

}
