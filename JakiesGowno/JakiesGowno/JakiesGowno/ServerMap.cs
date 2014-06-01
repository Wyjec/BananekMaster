using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace JakiesGowno
{
    class ServerMap
    {
        GraphicsDevice graphic;
        Camera camera;

        long seed = 0;

        public long[] seedsBuffer = new long[9];
        public Area[] activeAreas = new Area[4];
        public ServerMap()
        {
            for (int i = 0; i < 4; i++)
                activeAreas[i] = new Area();
            camera = new Camera();
        }

        public void Initialize(GraphicsDevice graphic, Vector2 playerPos, long seed = 0)
        {
            camera.Initialize(new Rectangle((int)playerPos.X - graphic.Viewport.TitleSafeArea.Width / 2, (int)playerPos.Y - graphic.Viewport.TitleSafeArea.Height / 2, graphic.Viewport.TitleSafeArea.Width, graphic.Viewport.TitleSafeArea.Height));
            this.seed = seed;
            this.graphic = graphic;
        }

        public void SetSeed(long seed)
        {
            this.seed = seed;
        }

        public void GenerateMap(ContentManager content)
        {                                    
            int curRow = ActiveAreaRow(camera.GetCorner());
            int curCol = ActiveAreaCol(camera.GetCorner());

            if (curRow < -1)
            {
                for (int i = 0; i < 4; i++)
                {                    
                    activeAreas[i].Initialize(Area.Type.Sky);
                    activeAreas[i].seed = seedsBuffer[4 + i%2 + i/2*3 + camera.seedOffsetX + camera.seedOffsetY * 3];
                    activeAreas[i].GenerateSky(content);
                }
            }
            else if (curRow == -1)
            {
                for (int i = 0; i < 2; i++)
                {
                    activeAreas[i].Initialize(Area.Type.Sky);
                    activeAreas[i].seed = seedsBuffer[4 + i % 2 + i / 2 * 3 + camera.seedOffsetX + camera.seedOffsetY * 3];
                    activeAreas[i].GenerateSky(content);
                }
                for (int i = 2; i < 4; i++)
                {
                    activeAreas[i].Initialize(Area.Type.Wood);
                    activeAreas[i].seed = seedsBuffer[4 + i % 2 + i / 2 * 3 + camera.seedOffsetX + camera.seedOffsetY * 3];
                    activeAreas[i].GenerateTurbulenceArea(content, curCol + i % 2, seed);
                }
            }
            else if (curRow == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    activeAreas[i].Initialize(Area.Type.Wood);
                    activeAreas[i].seed = seedsBuffer[4 + i % 2 + i / 2 * 3 + camera.seedOffsetX + camera.seedOffsetY * 3];
                    activeAreas[i].GenerateTurbulenceArea(content, curCol + i % 2, seed);
                }
                for (int i = 2; i < 4; i++)
                {
                    activeAreas[i].Initialize(Area.Type.Underground);
                    activeAreas[i].seed = seedsBuffer[4 + i % 2 + i / 2 * 3 + camera.seedOffsetX + camera.seedOffsetY * 3];
                    activeAreas[i].GenerateSolid(content);
                }

            }
            else if (curRow > 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    activeAreas[i].Initialize(Area.Type.Underground);
                    activeAreas[i].seed = seedsBuffer[4 + i % 2 + i / 2 * 3 + camera.seedOffsetX + camera.seedOffsetY * 3];
                    activeAreas[i].GenerateSolid(content);
                }
            }
        }

        public bool Update(Vector2 playerPos, ContentManager content)
        {
            camera.Update(new Rectangle((int)playerPos.X - graphic.Viewport.TitleSafeArea.Width / 2, (int)playerPos.Y - graphic.Viewport.TitleSafeArea.Height / 2, graphic.Viewport.TitleSafeArea.Width, graphic.Viewport.TitleSafeArea.Height));
            if (camera.ChangedColRow())
                GenerateMap(content);

            return camera.CenterChangedColRow();
        }

        public bool CheckCollision(Rectangle colBox)
        {
            int curCol = ActiveAreaCol(camera.GetCorner());
            int curRow = ActiveAreaRow(camera.GetCorner());            
            //int activeRow = ActiveAreaRow(new Vector2(colBox.X, colBox.Y));
            //int activeCol = ActiveAreaCol(new Vector2(colBox.X, colBox.Y));
            
            for (int i = 0; i < 4; i++)
            {
                Rectangle colRect = new Rectangle((colBox.X) - (i % 2 + curCol ) * Tile.width * Area.widthTiles, (colBox.Y) - (i / 2 + curRow) * Tile.height * Area.heightTiles, colBox.Width, colBox.Height);

                if (activeAreas[i].CheckCollision(colRect))
                    return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 playerPos)
        {
            int curCol = ActiveAreaCol(camera.GetCorner());
            int curRow = ActiveAreaRow(camera.GetCorner());       
            for (int i = 0; i < 4; i++)
            {
                float shiftX = (i % 2 + curCol) * Tile.width * Area.widthTiles - camera.cameraRect.X;
                float shiftY = (i / 2 + curRow) * Tile.height * Area.heightTiles - camera.cameraRect.Y;
                activeAreas[i].Draw(spriteBatch, new Vector2(shiftX, shiftY));
            }
        }

        public long CurrentAreaSeed(Vector2 pos)
        {
            long seed = 0;
            int i = 0;

            i = Math.Abs(((int)pos.X % (2 * Area.widthTiles * Tile.width)) / (Area.widthTiles * Tile.width));
            i += Math.Abs((((int)pos.Y % (2 * Area.heightTiles * Tile.height)) / (Area.widthTiles * Tile.width)) * 2);
            return activeAreas[i].seed;
        }

        public static int ActiveAreaCol(Vector2 pos)
        {
            int temp = (int)pos.X;
            if (temp < 0)
                temp -= Area.widthTiles * Tile.width;
            return ((int)temp) / (Area.widthTiles * Tile.width);
        }

        public static int ActiveAreaRow(Vector2 pos)
        {
            int temp = (int)pos.Y;
            if (temp < 0)
                temp -= Area.heightTiles * Tile.height;
            return ((int)temp) / (Area.heightTiles * Tile.height);
        }        
    }
}
