using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace rokketz
{
    class Entity
    {
        const float maxSpeedY = 1000.0f;
        const float maxSpeedX = 1000.0f;

        public bool exist = true;
        public Vector2 position;
        public Vector2 speed;       
        public Vector2 acceleration;
        public Vector2 size;
        public float rotation = 0.0f;
        public Texture2D texture;    
        
        public Entity (Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
            this.speed = Vector2.Zero;
            this.acceleration = Vector2.Zero;
            this.size = new Vector2(texture.Width, texture.Height);
        }

        public Entity (Vector2 position, Texture2D texture, Vector2 speed)
        {
            this.position = position;
            this.texture = texture;
            this.speed = speed;
            this.acceleration = Vector2.Zero;
            this.size = new Vector2(texture.Width, texture.Height);
        }

        public Entity (Vector2 position, Texture2D texture, Vector2 speed, Vector2 acceleration)
        {
            this.position = position;
            this.texture = texture;
            this.speed = speed;
            this.acceleration = acceleration;
            this.size = new Vector2(texture.Width, texture.Height);
        }

        public Entity(Vector2 position, Texture2D texture, Vector2 speed, Vector2 acceleration, Vector2 size)
        {
            this.position = position;
            this.texture = texture;
            this.speed = speed;
            this.acceleration = acceleration;
            this.size = size;
        }

        public virtual void Update(GameTime gameTime, Rectangle bounds)
        {
            if (!exist)
                return;

            float elapsedTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            speed += elapsedTimeSeconds * acceleration;
            speed.X = MathHelper.Clamp(speed.X, -maxSpeedX, maxSpeedX);
            speed.Y = MathHelper.Clamp(speed.Y, -maxSpeedY, maxSpeedY);

            position += elapsedTimeSeconds * speed;

            if (position.X > (bounds.X + bounds.Width + size.X))
            {
                position.X = bounds.X - size.X;
            }
            else if (position.X < bounds.X - size.X)
            {
                position.X = bounds.X + bounds.Width + size.X;
            }

            if (position.Y > (bounds.Y + bounds.Height + size.Y))
            {
                position.Y = bounds.Y - size.Y;
            }
            else if (position.Y < bounds.Y - size.Y)
            {
                position.Y = bounds.Y + bounds.Height + size.Y;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!exist)
                return;

            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                             null, Color.White, rotation, size, SpriteEffects.None, 0);
        }

        public virtual bool CheckCollision(Entity entity)
        {
            Rectangle myColBox = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle colBox = new Rectangle((int)entity.position.X, (int)entity.position.Y, (int)entity.size.X, (int)entity.size.Y);
            return myColBox.Intersects(colBox); 
        }

    }
}
