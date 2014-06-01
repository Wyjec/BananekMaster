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
    class Item
    {
        public string id;
        public string assetName;
        private Texture2D texture2d;
        private string fullName;
        private Vector2 position;

        public Item(string id, string assetName, string fullname, Vector2 position)
        {
            this.id = id;
            this.assetName = assetName;
            this.fullName = fullname;
            this.position = position;
        }

        public Item(string xmlFile, string id, Vector2 position)
        {
            XmlDocument items = new XmlDocument();
            items.Load(xmlFile);

            XmlNodeList itemsList = items.GetElementsByTagName("item");

            foreach (XmlNode item in itemsList)
            {
                if (item.SelectSingleNode("id").Value == id)
                {
                    this.id = id;
                    this.assetName = item.SelectSingleNode("image").Value;
                    this.fullName = item.SelectSingleNode("name").Value;
                    this.position = position;
                    break;
                }
            }
        }

        public bool OnLoad(ContentManager cm)
        {
            texture2d = cm.Load<Texture2D>(assetName);
            if (texture2d == null)
                return false;
            return true;
        }

        public void OnDraw(SpriteBatch sb)
        {
            sb.Draw(texture2d, position, Color.White);
        }
    }
}
