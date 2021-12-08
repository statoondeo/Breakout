using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IAssetService : IService
	{
		void Load(ContentManager content, GraphicsDevice graphicDevice);
		void UnLoad();
		Texture2D GetTexture(TextureName name);
		SpriteFont GetFont(FontName name);
	}
}

