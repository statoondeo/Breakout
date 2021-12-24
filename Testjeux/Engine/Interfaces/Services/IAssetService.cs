using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public interface IAssetService : IService
	{
		void Load(ContentManager content, GraphicsDevice graphicDevice);
		void UnLoad();
		Texture2D GetTexture(TextureName name);
		SoundEffect GetSound(SoundName name);
		SpriteFont GetFont(FontName name);
		Song GetMusic(MusicName name);
	}
}

