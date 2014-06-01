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
    class Player : Entity
    {
        const float acc = 250.0f;
        const float spd = 30.0f;
        const float rotSpd = 1.0f;
  
        public float rotSpeed = 0.0f;

        public Player (Vector2 position, Texture2D texture) : base(position, texture) { }
        public Player (Vector2 position, Texture2D texture, Vector2 speed) : base(position, texture, speed) { }
        public Player (Vector2 position, Texture2D texture, Vector2 speed, Vector2 acceleration) : base (position, texture, speed, acceleration) { }
        public Player (Vector2 position, Texture2D texture, Vector2 speed, Vector2 acceleration, Vector2 size) : base(position, texture, speed, acceleration, size) { }

        public void UpdateControl(KeyboardState keyboardstate, KeyboardState previousKeyboardState)
        {
            if (!exist)
                return;

            Vector2 rotVector = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            if (keyboardstate.IsKeyDown(Keys.Up))
            {
                if (previousKeyboardState.IsKeyUp(Keys.Up))
                    speed += spd * rotVector;
                acceleration = acc * rotVector;
            }
            else if (keyboardstate.IsKeyDown(Keys.Down))
            {
                if (previousKeyboardState.IsKeyUp(Keys.Down))
                    speed -= spd * rotVector;
                acceleration = -acc * rotVector;
            }
            else
                acceleration = Vector2.Zero;

            if (keyboardstate.IsKeyDown(Keys.Left))
                rotSpeed = -rotSpd;
            else if (keyboardstate.IsKeyDown(Keys.Right))
                rotSpeed = rotSpd;
            else
                rotSpeed = 0.0f;
        }

        public override void Update(GameTime gameTime, Rectangle bounds)
        {
            if (!exist)
                return;

            rotation += rotSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotation = MathHelper.WrapAngle(rotation);
            base.Update(gameTime, bounds);
        }
    }
}
