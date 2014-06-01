using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JakiesGowno
{
    class Animation
    {        
        public Texture2D texture;
        int frames;
        public int width; // -1 means auto
        public int height; // -1 means auto
        int actualFrame;
        TimeSpan frameTime;
        TimeSpan previousTime;
        bool enabled;
        bool looped;

        public void Initialize(Texture2D texture, int frames, TimeSpan frameTime, bool looped, int width = -1, int height = -1)
        {
            this.enabled = true;
            this.frames = frames;
            this.frameTime = frameTime;
            this.looped = looped;
            this.texture = texture;
            this.width = width;
            this.height = height;
            previousTime = new TimeSpan();
            actualFrame = 0;
            if (width == -1)
                this.width = texture.Width / frames;
            if (height == -1)
                this.height = texture.Height;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousTime > frameTime)
            {
                previousTime = gameTime.TotalGameTime;
                if (++actualFrame == frames)
                {
                    if (looped)
                        actualFrame = 0;
                    else
                        enabled = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle dstRect = new Rectangle((int)position.X - width / 2, (int)position.Y - height / 2, width, height);
            if (!enabled)
                return;
            Rectangle srcRect = new Rectangle(actualFrame * width, 0, width, height);
            spriteBatch.Draw(texture, dstRect, srcRect, Color.White);
        }        
    }
}
