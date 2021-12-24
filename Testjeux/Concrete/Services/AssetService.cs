using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public class AssetService : IAssetService
    {
        // Stockage des ressources
        protected IDictionary<TextureName, Texture2D> TexturesDictionary;
        protected IDictionary<SoundName, SoundEffect> SoundsDictionary;
        protected IDictionary<MusicName, Song> MusicsDictionary;
        protected IDictionary<FontName, SpriteFont> FontsDictionary;

        public AssetService() 
        {
            TexturesDictionary = new Dictionary<TextureName, Texture2D>();
            SoundsDictionary = new Dictionary<SoundName, SoundEffect>();
            MusicsDictionary = new Dictionary<MusicName, Song>();
            FontsDictionary = new Dictionary<FontName, SpriteFont>();
        }

        public Texture2D GetTexture(TextureName name)
		{
            return (TexturesDictionary[name]);
		}

        public SoundEffect GetSound(SoundName name)
        {
            return (SoundsDictionary[name]);
        }
        public Song GetMusic(MusicName name)
        {
            return (MusicsDictionary[name]);
        }

        public SpriteFont GetFont(FontName name)
        {
            return (FontsDictionary[name]);
        }

        public void Load(ContentManager content, GraphicsDevice graphicDevice)
		{
            // Chargement des fonts
            foreach (FontName fontName in (FontName[])Enum.GetValues(typeof(FontName)))
            {
                FontsDictionary.Add(fontName, content.Load<SpriteFont>("Fonts/" + fontName.ToString()));
            }

            // Chargement des sons
            foreach (SoundName soundName in (SoundName[])Enum.GetValues(typeof(SoundName)))
            {
                SoundsDictionary.Add(soundName, content.Load<SoundEffect>("Sounds/" + soundName.ToString()));
            }

            // Chargement des musics
            foreach (MusicName musicName in (MusicName[])Enum.GetValues(typeof(MusicName)))
            {
                MusicsDictionary.Add(musicName, content.Load<Song>("Musics/" + musicName.ToString()));
            }

            // Chargement des textures
            foreach (TextureName textureName in (TextureName[])Enum.GetValues(typeof(TextureName)))
            {
                if (textureName != TextureName.Drawable)
                {
                    TexturesDictionary.Add(textureName, content.Load<Texture2D>("Textures/" + textureName.ToString()));
                }
            }

            // Texture manuelle pour les collideBox
            Texture2D DrawableTexture = new Texture2D(graphicDevice, 1, 1, false, SurfaceFormat.Color);
            DrawableTexture.SetData(Enumerable.Repeat(Color.White, 1).ToArray());
            TexturesDictionary.Add(TextureName.Drawable, DrawableTexture);
        }

        public void UnLoad()
		{
            // Libération des textures
            foreach (KeyValuePair<TextureName, Texture2D> kvp in TexturesDictionary)
            {
                kvp.Value.Dispose();
            }

            // Libération des sons
            foreach (KeyValuePair<SoundName, SoundEffect> kvp in SoundsDictionary)
            {
                kvp.Value.Dispose();
            }
        }
    }
}
