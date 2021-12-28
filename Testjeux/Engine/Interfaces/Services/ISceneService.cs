using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface ISceneService : IService, IScene
	{
		int Life { get; set; }
		int MaxLife { get; }
		int Level { get; set; }
		SceneModeNames Mode { get; set; }
		bool ExitRequired { get; set; }
		IScene CurrentScene { get; }
		IScene ChangeScene(SceneType scene, ICommand whenLoadedCommand);
		IScene ChangeScene(SceneType scene, int levelNumber, ICommand whenLoadedCommand);
		CamShake CamShake { get; }
	}
}

