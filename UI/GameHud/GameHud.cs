using Godot;
using System;
using System.ComponentModel;

public partial class GameHud : Control
{
	[Export] private ColorRect _colorRect;
	[Export] private Label _gameOverLabel;
	[Export] private Timer _completeTimer; 
	[Export] private AudioStreamPlayer _winSound;
	[Export] private AudioStreamPlayer _gameoverSound;

	private bool _canContinue = false; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_colorRect.Hide();
		SignalHub.Instance.OnLevelComplete += OnLevelComplete;
		_completeTimer.Timeout += OncompletedTimerTimeout;
	}

    public override void _ExitTree()
    {
		SignalHub.Instance.OnLevelComplete -= OnLevelComplete;
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

	// signal Methods 

	private void OnLevelComplete()
    {	GetTree().Paused = true;
		_colorRect.Show();
		_gameOverLabel.Text = "Level Complete";
		_completeTimer.Start();
		_winSound.Play();
    }

	private void OncompletedTimerTimeout()
	{
		_canContinue = true;
	}

}
