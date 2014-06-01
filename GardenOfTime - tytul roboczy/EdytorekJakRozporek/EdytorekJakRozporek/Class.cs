using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;

namespace EdytorekJakRozporek
{
    public class Item
    {
        [XmlAttribute("Id")]
        public string id { get; set; }
        [XmlElement("Asset")]
        public string asset { get; set; }
        public string assetName { get; set; }
        [XmlElement("Name")]
        public string fullName { get; set; }

        public Item()
        {
        }

        public Item(string id, string assetName, string fullName = "Default Name")
        {
            this.id = id;
            this.assetName = assetName;
            this.fullName = fullName;
            this.asset = Path.GetFileName(assetName);
        }
    }

    public class Level
    {
        [XmlAttribute("Id")]
        public string id { get; set; }
        [XmlElement("Time")]
        public int time { get; set; }
        [XmlElement("Items")]
        public int items { get; set; }
        [XmlElement("Map")]
        public string mapId { get; set; }

        public Level()
        {
        }

        public Level(string id, int time = 300, int items = 10, string mapId = "DEFAULT")
        {
            this.id = id;
            this.time = time;
            this.items = items;
            this.mapId = mapId;
        }

    }

    public struct ItemInfo
    {
        [XmlAttribute("Id")]
        public string id;
        [XmlElement("Placement")]
        public Rectangle placement;
        [XmlElement("Rotation")]
        public int rotation;
    }

    public class Map
    {
        [XmlAttribute("Id")]
        public string id { get; set; }
        [XmlElement("Name")]
        public string name { get; set; }
        [XmlElement("Asset")]
        public string asset { get; set; }
        public string background { get; set; }
        [XmlElement("Item")]
        public List<ItemInfo> allowedItems { get; set; }

        public Map()
        {
        }

        public Map(string id , string name = "Default name", string background = "DEFAULT")
        {
            this.id = id;
            this.name = name;
            this.background = background;
            this.asset = Path.GetFileName(background);
            allowedItems = new List<ItemInfo>();
            allowedItems.Clear();
        }

        public void AddItem(string id, Rectangle placement, int rotation = 0)
        {
            ItemInfo sItemInfo;
            sItemInfo.id = id;
            sItemInfo.placement = placement;
            sItemInfo.rotation = rotation;
            for(int i = 0; i < allowedItems.Count;i++)
            {
                if (allowedItems.ElementAt<ItemInfo>(i).id == id)
                {
                    allowedItems.RemoveAt(i);
                    allowedItems.Insert(i, sItemInfo);
                    return;
                }                 
            }
            allowedItems.Add(sItemInfo);         
        }        
    }
}
