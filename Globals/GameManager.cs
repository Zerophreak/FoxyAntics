using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance {get; private set; }
	
	private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");
	private PackedScene _levelScene = GD.Load<PackedScene>("res://Scenes/LevelBase/LevelBase.tscn");
	public override void _Ready()
	{
		Instance = this;
	}

	public void LoadMainScene()
	{
		GetTree().ChangeSceneToPacked(_mainScene);
	}

	public void LoadlevelScene()
	{
		GetTree().ChangeSceneToPacked(_levelScene);
	}

	public static void ChangeToMain()
	{
		Instance.LoadMainScene();
	}

	public static void ChangeToLevel()
	{
		Instance.LoadlevelScene();
	}
}