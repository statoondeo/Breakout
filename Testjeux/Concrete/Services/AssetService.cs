using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class AssetService : IAssetService
    {
        protected IDictionary<TextureName, Texture2D> TexturesDictionary;
        protected IDictionary<FontName, SpriteFont> FontsDictionary;

        public AssetService() 
        {
            TexturesDictionary = new Dictionary<TextureName, Texture2D>();
            FontsDictionary = new Dictionary<FontName, SpriteFont>();
        }
        public Texture2D GetTexture(TextureName name)
		{
            return (TexturesDictionary[name]);
		}

        public SpriteFont GetFont(FontName name)
		{
            return (FontsDictionary[name]);
        }

        public void Load(ContentManager content, GraphicsDevice graphicDevice)
		{
            FontsDictionary.Add(FontName.Title, content.Load<SpriteFont>("Neuropol48"));
            FontsDictionary.Add(FontName.Button, content.Load<SpriteFont>("Neuropol24"));

            TexturesDictionary.Add(TextureName.RedBall, content.Load<Texture2D>("redball"));
            TexturesDictionary.Add(TextureName.GrayBall, content.Load<Texture2D>("GrayBall"));
            TexturesDictionary.Add(TextureName.PurpleBall, content.Load<Texture2D>("purpleball"));
            Texture2D DrawableTexture = new Texture2D(graphicDevice, 1, 1, false, SurfaceFormat.Color);
            DrawableTexture.SetData(Enumerable.Repeat(Color.White, 1).ToArray());
            TexturesDictionary.Add(TextureName.Drawable, DrawableTexture);
        }

        public void UnLoad()
		{
			foreach (KeyValuePair<TextureName, Texture2D> kvp in TexturesDictionary)
			{
                kvp.Value.Dispose();

            }
        }
    }
}
