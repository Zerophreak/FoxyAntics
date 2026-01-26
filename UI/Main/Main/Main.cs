using Godot;
using System;

public partial class Main : Control
{


	private PackedScene _highscoreLabel = GD.Load<PackedScene>("res://UI/Main/HighscoreLabel.tscn");

	[Export] private GridContainer _gridContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScoreManager.SimulatingAddingAndSavingSomeScores();
		SetScores();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SetScores()
	{
		foreach (GameScore score in ScoreManager.Instance.ScoresHistory)
		{
			var hsl = _highscoreLabel.Instantiate<HighscoreLabel>();
			_gridContainer.AddChild(hsl);
			hsl.ScoresHistoryText(score);
		}
	}
}
