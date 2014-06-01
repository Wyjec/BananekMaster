using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Ionic.Zip;
using Ionic.Zlib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GardenOfTime
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {   
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Item test;

        public Game1()
        {            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void LoadLevel(string fileName)
        {
            if (fileName != "")
            {

                string tempPath = Path.GetTempPath();
                try
                {
                    int i = 0;
                    string directory;
                    ZipFile zip = new ZipFile(fileName);
                    do
                    {
                        directory = tempPath + fileName + i++;
                    } while (Directory.Exists(directory));
                    directory = directory + "\\";
                    Directory.CreateDirectory(directory);

                    zip.ExtractAll(directory);
                    XPathDocument oXPathDocument = new XPathDocument(directory + "level.xml");
                    XPathNavigator oXPathNavigator = oXPathDocument.CreateNavigator();
                    XPathNodeIterator oNodesIterator = oXPathNavigator.Select("/Levels/Level");

                    foreach (XPathNavigator oLevel in oNodesIterator)
                    {
                        Level lvl = new Level(oLevel.GetAttribute("Id", ""),
                                    int.Parse(oLevel.SelectSingleNode("Time").Value),
                                    int.Parse(oLevel.SelectSingleNode("Items").Value),
                                                oLevel.SelectSingleNode("Map").Value);
                        oLevelList.Add(lvl);
                    }

                    oXPathDocument = new XPathDocument(directory + "map.xml");
                    oXPathNavigator = oXPathDocument.CreateNavigator();
                    oNodesIterator = oXPathNavigator.Select("/Maps/Map");

                    foreach (XPathNavigator oMap in oNodesIterator)
                    {
                        Map map = new Map(oMap.GetAttribute("Id", ""),
                                            oMap.SelectSingleNode("Name").Value,
                                            directory + oMap.SelectSingleNode("Asset"));

                        XPathNavigator oXPathItemNavigator = oMap.CreateNavigator();
                        XPathNodeIterator oItemsNodesIterator = oMap.Select("Item");
                        foreach (XPathNavigator oMapItem in oItemsNodesIterator)
                        {
                            ItemInfo ii;
                            ii.id = oMapItem.GetAttribute("Id", "");
                            ii.rotation = int.Parse(oMapItem.SelectSingleNode("Rotation").Value);
                            ii.placement = new Rectangle(int.Parse(oMapItem.SelectSingleNode("Placement/X").Value),
                                                            int.Parse(oMapItem.SelectSingleNode("Placement/Y").Value),
                                                            int.Parse(oMapItem.SelectSingleNode("Placement/Width").Value),
                                                            int.Parse(oMapItem.SelectSingleNode("Placement/Height").Value));
                            map.allowedItems.Add(ii);
                        }
                        oMapList.Add(map);
                    }

                    oXPathDocument = new XPathDocument(directory + "item.xml");
                    oXPathNavigator = oXPathDocument.CreateNavigator();
                    oNodesIterator = oXPathNavigator.Select("/Items/Item");

                    foreach (XPathNavigator oItem in oNodesIterator)
                    {
                        Item item = new Item(oItem.GetAttribute("Id", ""),
                                                directory + oItem.SelectSingleNode("Asset").Value,
                                                oItem.SelectSingleNode("Name").Value);
                        oItemList.Add(item);
                    }

                    ReloadItemList();
                    ReloadLevelList();
                    ReloadMapList();
                    MessageBox.Show("Successfully loaded level pack.\nPath: " + fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Openfile error: " + ex.Message);
                }
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here            
            base.Initialize();            
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            test.OnDraw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
