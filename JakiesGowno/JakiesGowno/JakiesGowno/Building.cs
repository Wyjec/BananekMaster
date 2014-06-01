using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JakiesGowno
{
    class Building
    {
        Texture2D outside;
        Texture2D inside;
        Vector2 position;
        public bool isInside;
        public int enterTile;
        public int tileWidth
        {
            get { return outside.Width / Tile.width; }
        }

        public int tileHeight
        {
            get { return outside.Height / Tile.height; }
        }

        public void Initialize(Texture2D outside, Texture2D inside, Vector2 position, int enterTile)
        {
            this.outside = outside;
            this.inside = inside;
            this.enterTile = enterTile;
            this.position = position;
            isInside = false;
        }

        public bool Enter(Rectangle player)
        {
            Rectangle door = new Rectangle((int)position.X + (enterTile % tileWidth) * Tile.width, (int)position.Y + (enterTile / tileWidth) * Tile.height, Tile.width, Tile.height);
            if (door.Intersects(player))
            {
                isInside = !isInside;
                return true;
            }
            return false;
        }

        public bool CheckCollision(Rectangle colBox)
        {
            Rectangle floor = new Rectangle((int)position.X, (int)position.Y + tileHeight * Tile.height, tileWidth * Tile.width, 1);
            Rectangle wallLeft = new Rectangle((int)position.X - 1, (int)position.Y, 1, tileHeight * Tile.height);
            Rectangle wallRight = new Rectangle((int)position.X + tileWidth * Tile.width, (int)position.Y, 1, tileHeight * Tile.height);
            Rectangle cell = new Rectangle((int)position.X, (int)position.Y - 1, tileWidth * Tile.width, 1);
            
            if (floor.Intersects(colBox))
                return true;
            if (cell.Intersects(colBox))
                return true;
            if (wallLeft.Intersects(colBox))
                return true;
            if (wallRight.Intersects(colBox))
                return true;

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 camera)
        {
            if (isInside)
                spriteBatch.Draw(inside, new Rectangle((int)position.X - (int)camera.X, (int)position.Y - (int)camera.Y, inside.Width, inside.Height), Color.White);
            else
                spriteBatch.Draw(outside, new Rectangle((int)position.X - (int)camera.X, (int)position.Y - (int)camera.Y, outside.Width, outside.Height), Color.White);
        }
    }
}
