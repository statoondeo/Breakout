namespace GameNameSpace
{
	public interface ISceneService : IService, IScene
	{
		IScene ChangeScene(SceneType scene);
		IScene ChangeScene(SceneType scene, int levelNumber);
	}
}

