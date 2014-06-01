using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace JakiesGowno
{
    public enum Render
    {
        Fixed,
        Normal
    }

    class Player
    {
        public byte id;
        public Animation animation;
        public Vector2 position;
        public Vector2 speed;
        public Vector2 acc;
        public bool doubleJump;
        public bool inAir;
        public string nick;

        public int actualHealth;
        public int maxHealth;

        public int width
        {
            get { return animation.width; }
        }

        public int height
        {
            get { return animation.height; }
        }

        public void Initialization(Animation animation, Vector2 position, Vector2 speed, Vector2 acc, string nick, byte id = 0)
        {
            this.doubleJump = false;
            this.inAir = false;
            this.animation = animation;
            this.position = position;
            this.speed = speed;
            this.acc = acc;
            this.id = id;
            this.maxHealth = 200;
            this.actualHealth = 130;
            this.nick = nick;
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Render type, Vector2 camera, Texture2D pixelTexture, SpriteFont nickFont)
        {
            if (type == Render.Fixed)
            {
                // draw player sprite
                animation.Draw(spriteBatch, camera);                
                // draw hp bars
                Rectangle green = new Rectangle((int)camera.X - width/2, (int)camera.Y - height / 2 - 10, (int)(((float)actualHealth / maxHealth) * 30), 5);
                Rectangle red = new Rectangle(green.X + green.Width, green.Y, 30 - green.Width, 5);
                spriteBatch.Draw(pixelTexture, green, Color.Green);
                spriteBatch.Draw(pixelTexture, red, Color.Red);
                // draw nickname
                spriteBatch.DrawString(nickFont, nick, new Vector2((int)camera.X - width / 2, (int)camera.Y - height / 2 - 25), Color.White);
            }
            else if (type == Render.Normal)
            {
                // draw player sprite
                animation.Draw(spriteBatch, new Vector2(position.X - camera.X, position.Y - camera.Y));                
                // draw hp bars
                Rectangle green = new Rectangle((int)position.X - (int)camera.X - width / 2, (int)position.Y - (int)camera.Y - height / 2 - 10, (int)(((float)actualHealth / maxHealth) * 30), 5);
                Rectangle red = new Rectangle(green.X + green.Width, green.Y, 30 - green.Width, 5);
                spriteBatch.Draw(pixelTexture, green, Color.Green);
                spriteBatch.Draw(pixelTexture, red, Color.Red);
                // draw nickname
                spriteBatch.DrawString(nickFont, nick, new Vector2((int)position.X - (int)camera.X - width / 2, (int)position.Y - (int)camera.Y - height / 2 - 25 ), Color.White);
            }
        }
    }
}
