using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace JakiesGowno
{
    class Tile
    {
        public enum Type
        {
            Solid,
            Background
        }
        public const int width = 40;
        public const int height = 40;
        public Texture2D tileTexture;
        public Type type;

        public void Initialize(Texture2D tileTexture, Type type)
        {
            this.type = type;
            this.tileTexture = tileTexture;
        }
    }
}
