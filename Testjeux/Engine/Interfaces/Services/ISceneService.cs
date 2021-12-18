namespace GameNameSpace
{
	public interface ISceneService : IService, IScene
	{
		bool ExitRequired { get; set; }
		IScene CurrentScene { get; }
		IScene ChangeScene(SceneType scene, ICommand whenLoadedCommand);
		IScene ChangeScene(SceneType scene, int levelNumber, ICommand whenLoadedCommand);
	}
}

