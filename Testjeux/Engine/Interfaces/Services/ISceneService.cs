namespace GameNameSpace
{
	public interface ISceneService : IService, IScene
	{
		float TotalGameTime { get; }
		int Life { get; set; }
		int MaxLife { get; }
		int Level { get; set; }
		SceneModeNames Mode { get; set; }
		bool ExitRequired { get; set; }
		IScene CurrentScene { get; }
		IScene ChangeScene(SceneType scene, ICommand whenLoadedCommand);
		IScene ChangeScene(SceneType scene, int levelNumber, ICommand whenLoadedCommand);
		IScene ChangeScene(SceneType scene, ParsedLevel level, ICommand whenLoadedCommand);
		CamShake CamShake { get; }
		FrameCounter FrameCounter { get; }
	}
}

