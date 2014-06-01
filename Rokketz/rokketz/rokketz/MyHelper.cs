using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace rokketz
{
    public static class MyHelper
    {
        public static bool KeyPressed(KeyboardState current, KeyboardState previous, Keys key)
        {
            if (current.IsKeyDown(key) && previous.IsKeyUp(key))
                return true;
            return false;
        }

        public static bool KeyReleased(KeyboardState current, KeyboardState previous, Keys key)
        {
            if (current.IsKeyUp(key) && previous.IsKeyDown(key))
                return true;
            return false;
        }
    }
}
