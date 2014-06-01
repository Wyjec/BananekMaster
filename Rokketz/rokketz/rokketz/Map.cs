using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace rokketz
{
    class Map
    {
        private Tile[,] mapTiles;
        private int width;
        private int height;

        public Map(int width = 1, int height = 1)
        {
            this.width = width;
            this.height = height;
            mapTiles = new Tile[width, height];
        }

        public bool LoadMap(string mapfilename)
        {
            return true;
        }

        public void GenerateMap(int seed, ContentManager content)
        {
            Random rnd = new Random(seed);
            Texture2D solidTexture = content.Load<Texture2D>("kwadrat");

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if ((rnd.Next() % 2) == 1)
                        mapTiles[i, j] = new Tile(solidTexture, Type.Solid);
                  
                    else
                        mapTiles[i, j] = new Tile(null, Type.Background);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (mapTiles[i, j].tileTexture == null)
                        continue;
                    Rectangle destRect = new Rectangle(i*Tile.WIDTH, j*Tile.HEIGHT, Tile.WIDTH, Tile.HEIGHT);
                    spriteBatch.Draw(mapTiles[i, j].tileTexture, destRect, Color.White);
                }
            }
        }
    }
}
