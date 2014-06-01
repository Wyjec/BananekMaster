using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LibNoise.Xna;
using LibNoise.Xna.Operator;
using LibNoise.Xna.Generator;

namespace JakiesGowno
{
    public enum DigDirection
    {
        Left,
        Right,
        Down,
        Up
    }

    class Area
    {
        public enum Type
        {
            Underground,
            Sky,
            Wood,
            Start,
            Other
        }

        public long seed;
        public static int widthTiles = 35;
        public static int heightTiles = 35;
        public Type areaType;
        List<Tile> tiles;

        public void Initialize(Type type = Type.Other)
        {
            tiles = new List<Tile>();
            this.areaType = type;
        }

        public void GenerateSky(ContentManager content)
        {
            this.areaType = Type.Sky;
            tiles.Clear();
            Tile tile;
            for (int i = 0; i < widthTiles * heightTiles; i++)
            {
                tile = new Tile();
                tile.Initialize(null, Tile.Type.Background);
                tiles.Add(tile);
            }
        }

        public void GenerateSolid(ContentManager content)
        {
            this.areaType = Type.Underground;
            Texture2D tileTexture;
            tiles.Clear();
            Tile tile;
            for (int i = 0; i < widthTiles * heightTiles; i++)
            {
                tile = new Tile();
                tileTexture = content.Load<Texture2D>("ziemia");
                tile.Initialize(tileTexture, Tile.Type.Solid);
                tiles.Add(tile);
            }
        }

        public void GenerateTurbulenceArea(ContentManager content, int currentColumn, long horizonSeed = 0, double varBase = 0.01)
        {

            this.areaType = Type.Wood;
            Turbulence turbulence = new Turbulence(0.1, new Perlin());
            int[,] genTiles = new int[widthTiles,heightTiles];
            Tile tile;
            Tile.Type tileType;
            Texture2D tileTexture;

            for (int i = 0; i < widthTiles; i++)
            {
                double turb = (turbulence.GetValue((i + widthTiles * currentColumn) * varBase + horizonSeed, 0, 0) + 1.0) / 2;
                int height = (int)(turb * heightTiles);
                for(int j = 0; j < heightTiles; j++)
                {
                    if(j < height)
                        genTiles[i, heightTiles - j - 1] = -1;
                    else if(j > height)
                        genTiles[i, heightTiles - j - 1] = 1;
                    else
                        genTiles[i, heightTiles - j - 1] = 0;
                }
            }
            for(int i = 0; i < widthTiles*heightTiles; i++)
            {
                tile = new Tile();
                if(genTiles[i % widthTiles, i / widthTiles] == -1)
                {
                    tileTexture = content.Load<Texture2D>("ziemia");
                    tileType = Tile.Type.Solid;
                }
                else if(genTiles[i % widthTiles, i / widthTiles] == 0)
                {
                    tileTexture = content.Load<Texture2D>("trawa1");
                    tileType = Tile.Type.Background;
                }
                else
                {
                    tileTexture = null;
                    tileType = Tile.Type.Background;
                }
                tile.Initialize(tileTexture, tileType);
                tiles.Add(tile);                
            }
        }

        public bool CheckCollision(Rectangle colBox)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].type == Tile.Type.Solid)
                {
                    Rectangle tileRect = new Rectangle(i % widthTiles * Tile.width, i / widthTiles * Tile.height, Tile.width, Tile.height);
                    if (colBox.Intersects(tileRect))
                        return true;                
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 shift)
        {            
            for (int i = 0; i < tiles.Count; i++)
            {
                if(tiles[i].tileTexture == null)
                    continue;

                Rectangle rect = new Rectangle((i % widthTiles) * Tile.width + (int)shift.X, (i / widthTiles) * Tile.height + (int)shift.Y, Tile.width, Tile.height);
                spriteBatch.Draw(tiles[i].tileTexture, rect, Color.White);
            }
        }

        public void ApplyDiffs(Dictionary<int, Tile> diffs)
        {
            foreach(KeyValuePair<int, Tile> areaDiff in diffs)
            {
                tiles[areaDiff.Key] = areaDiff.Value;
            }
        }

        public static int ActiveTile(Vector2 position)
        {
            int activeTile = -1;
            activeTile = ((int)position.X / Tile.width) + widthTiles * ((int)position.Y / Tile.width);
            return activeTile;
        }

    }
}
