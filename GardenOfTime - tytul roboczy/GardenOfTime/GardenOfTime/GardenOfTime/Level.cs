using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GardenOfTime
{
    class Level
    {

        public int id;
        //
        public int time;
        public int items;
        Map map;

        public Level(string xmlFile, int id)
        {
            XmlDocument levels = new XmlDocument();
            levels.Load(xmlFile);

            XmlNodeList levelsList = levels.GetElementsByTagName("level");

            foreach (XmlNode level in levelsList)
            {
                if (int.Parse(levels.SelectSingleNode("id").Value) == id)
                {
                    this.id = id;
                    this.time = int.Parse(levels.SelectSingleNode("time").Value);
                    this.items = int.Parse(levels.SelectSingleNode("items").Value);
                    break;
                }
            }
        }

        public void OnDraw(SpriteBatch sb)
        {
            map.OnDraw(sb);
        }
    }
}
