using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace rokketz
{
    public enum Type
    {
        Solid,
        Background
    }

    class Tile
    {
        public static int WIDTH = 100;
        public static int HEIGHT = 100;
        public Texture2D tileTexture;
        public Type type;

        public Tile(Texture2D tileTexture, Type type)
        {
            this.tileTexture = tileTexture;
            this.type = type;
        }
    }
}
