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
    class Hud
    {
        private int pageCapacity;
        private bool minimized;
        private List<String> itemList;
        private int actualPage; // actual page

        public Hud(int pageCapacity = 6)
        {
            this.pageCapacity = pageCapacity;
            minimized = false;
            itemList.Clear();
            actualPage = 0;
        }

        public void AddItem(string itemName)
        {
            itemList.Add(itemName);
            itemList.Sort();
        }

        public bool RemoveItem(string itemName)
        {
            return (itemList.Remove(itemName));
        }

        public void NextPage()
        {
            actualPage++;
            int pageCount = itemList.Count / pageCapacity;
            if (itemList.Count % pageCapacity > 0)
                pageCount++;
            if (actualPage > pageCount - 1)
                actualPage = 0;
        }

        public void OnEvent()
        {
        }

        public void OnDraw(SpriteBatch sb)
        {
        }
    }
}
