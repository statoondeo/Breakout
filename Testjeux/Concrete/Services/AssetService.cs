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
            FontsDictionary.Add(FontName.GiantTitle, content.Load<SpriteFont>("Neuropol1XX"));
            FontsDictionary.Add(FontName.BigTitle, content.Load<SpriteFont>("Neuropol106"));
            FontsDictionary.Add(FontName.Title, content.Load<SpriteFont>("Neuropol48"));
            FontsDictionary.Add(FontName.Button, content.Load<SpriteFont>("Neuropol24"));

            TexturesDictionary.Add(TextureName.Blob, content.Load<Texture2D>("blob"));
            TexturesDictionary.Add(TextureName.RedSpark, content.Load<Texture2D>("redspark"));
            TexturesDictionary.Add(TextureName.BlueSpark, content.Load<Texture2D>("bluespark"));
            TexturesDictionary.Add(TextureName.GreenSpark, content.Load<Texture2D>("greenspark"));
            TexturesDictionary.Add(TextureName.RedBullet, content.Load<Texture2D>("redbullet"));
            TexturesDictionary.Add(TextureName.Stars01, content.Load<Texture2D>("stars01"));
            TexturesDictionary.Add(TextureName.Stars02, content.Load<Texture2D>("stars02"));
            TexturesDictionary.Add(TextureName.Stars03, content.Load<Texture2D>("stars03"));
            TexturesDictionary.Add(TextureName.Gas1, content.Load<Texture2D>("gas1"));
            TexturesDictionary.Add(TextureName.Gas2, content.Load<Texture2D>("gas2"));
            TexturesDictionary.Add(TextureName.Gas3, content.Load<Texture2D>("gas3"));
            TexturesDictionary.Add(TextureName.Platform, content.Load<Texture2D>("platform"));
            TexturesDictionary.Add(TextureName.LaserGlow, content.Load<Texture2D>("laser_glow"));
            TexturesDictionary.Add(TextureName.Atom, content.Load<Texture2D>("atom"));
            TexturesDictionary.Add(TextureName.Wobbler, content.Load<Texture2D>("wobbler"));
            TexturesDictionary.Add(TextureName.Brain, content.Load<Texture2D>("brain"));
            TexturesDictionary.Add(TextureName.Shield, content.Load<Texture2D>("shield"));
            TexturesDictionary.Add(TextureName.Rocks, content.Load<Texture2D>("rocks"));
            TexturesDictionary.Add(TextureName.Thrust, content.Load<Texture2D>("thrust"));
            TexturesDictionary.Add(TextureName.BigPanel, content.Load<Texture2D>("bigPanel"));
            TexturesDictionary.Add(TextureName.Button, content.Load<Texture2D>("button"));

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
