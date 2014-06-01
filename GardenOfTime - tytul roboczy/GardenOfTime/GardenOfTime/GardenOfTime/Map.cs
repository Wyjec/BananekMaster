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
    struct ItemInfo
    {
        String id;
        Rectangle placement;
        int rotation;
    }

    class Map
    {
        String id;
        public String name;
        public string assetName;
        private Texture2D background;
        public List<ItemInfo> allowedItems;
        public List<Item>     items;
        public Map(string xmlFile, String id)
        {
            XmlDocument maps = new XmlDocument();
            maps.Load(xmlFile);

            XmlNodeList levelsList = maps.GetElementsByTagName("map");

            foreach (XmlNode level in levelsList)
            {
                if (maps.SelectSingleNode("id").Value == id)
                {
                    this.id = id;
                    this.name = maps.SelectSingleNode("name").Value;
                    this.assetName = maps.SelectSingleNode("background").Value;
                    break;
                }
            }
        }
        public bool OnLoad(ContentManager cm)
        {
            background = cm.Load<Texture2D>(assetName);
            if (background == null)
                return false;
            return true;
        }

        public void OnDraw(SpriteBatch sb)
        {
            sb.Draw(background, new Vector2(0, 0), Color.White);
            foreach (Item it in items)
                it.OnDraw(sb);
        }
    }
}
