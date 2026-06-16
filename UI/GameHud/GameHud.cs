using Godot;
using System;
using System.ComponentModel;

public partial class GameHud : Control
{
	[Export] private ColorRect _colorRect;
	[Export] private Label _gameOverLabel;
	[Export] private Label _ScoreLabel;
	[Export] private Timer _completeTimer; 
	[Export] private AudioStreamPlayer _winSound;
	[Export] private AudioStreamPlayer _gameoverSound;

	private bool _canContinue = false; 
	private int _score = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_colorRect.Hide();
		SignalHub.Instance.OnLevelComplete += OnLevelComplete;
		SignalHub.Instance.Onscored += OnScored;
		_completeTimer.Timeout += OncompletedTimerTimeout;

		UpdateScoreLabel(0);
	}

    public override void _ExitTree()
    {
		SignalHub.Instance.OnLevelComplete -= OnLevelComplete;
		SignalHub.Instance.Onscored -= OnScored;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("quit"))
		{
			GameManager.ChangeToMain();
		}
		if (_canContinue && @event.IsActionPressed("shoot"))
		{
			GameManager.ChangeToMain();

		}
	}

	private void UpdateScoreLabel(int points)
	{
		_ScoreLabel.Text = points.ToString("D4");
	}

	// signal Methods 

	private void OnLevelComplete()
    {	GetTree().Paused = true;
		_colorRect.Show();
		_gameOverLabel.Text = "Level Complete";
		_completeTimer.Start();
		_winSound.Play();
    }

	private void OnScored(int points)
	{
		_score += points;
		UpdateScoreLabel(_score);
	}

	private void OncompletedTimerTimeout()
	{
		_canContinue = true;
	}

}
