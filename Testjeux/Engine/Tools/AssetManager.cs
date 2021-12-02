using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class AssetManager
    {
        public SpriteFont Title { get; protected set; }
        public SpriteFont Button { get; protected set; }
        public Texture2D RedBall { get; protected set; }
        public Texture2D DrawableTexture { get; protected set; }

        public AssetManager()
		{

		}

        public void Load(ContentManager content, GraphicsDevice graphicDevice)
		{
            Title = content.Load<SpriteFont>("Neuropol48");
            Button = content.Load<SpriteFont>("Neuropol24");
            RedBall = content.Load<Texture2D>("redball");
            DrawableTexture = new Texture2D(graphicDevice, 1, 1, false, SurfaceFormat.Color);
            DrawableTexture.SetData(Enumerable.Repeat(Color.White, 1).ToArray());
        }

        public void UnLoad()
		{
            DrawableTexture.Dispose();
            RedBall.Dispose();
        }
    }
}
