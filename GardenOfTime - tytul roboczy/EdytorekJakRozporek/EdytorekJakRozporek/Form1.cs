using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;
using Ionic.Zlib;

namespace EdytorekJakRozporek
{
   
    public partial class Form1 : Form
    {
        const int MOTOROLA_DROID_WIDTH = 854;
        const int MOTOROLA_DROID_HEIGHT = 440;
        const int HTC_TOUCH_WIDTH = 800;
        const int HTC_TOUCH_HEIGHT = 480;

        List<Level> oLevelList = new List<Level>();
        List<Map> oMapList = new List<Map>();
        List<Item> oItemList = new List<Item>();

        Bitmap bmp;
        Image itemImg;
        Graphics gfx;

        public Form1()
        {
            InitializeComponent();
            try
            {
                bmp = new Bitmap(MOTOROLA_DROID_WIDTH, MOTOROLA_DROID_HEIGHT);
                pBox_map.Image = bmp;
                gfx = Graphics.FromImage(bmp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
            ReloadLevelList();
            ReloadMapList();
            ReloadItemList();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oLevelList.Clear();
            oItemList.Clear();
            oMapList.Clear();
            gfx.Clear(Color.White);
            pBox_map.Size = new Size(0, 0);
        }

        private void tSBtn_levelsAdd_Click(object sender, EventArgs e)
        {
            oLevelList.Add(new Level("LEVEL" + oLevelList.Count+1));

            ReloadLevelList();
        }

        private void ReloadLevelList()
        {
            listBox_levels.DataSource = null;
            listBox_levels.DataSource = oLevelList;
            listBox_levels.DisplayMember = "id";
        }

        private void ReloadMapList()
        {
            listBox_maps.DataSource = null;
            listBox_maps.DataSource = oMapList;
            listBox_maps.DisplayMember = "id";
        }

        private void ReloadItemList()
        {
            listBox_items.DataSource = null;
            listBox_items.DataSource = oItemList;
            listBox_items.DisplayMember = "id";
        }

        private void tSBtn_levelsRemove_Click(object sender, EventArgs e)
        {
            if (listBox_levels.SelectedIndex == -1)
                return;
            oLevelList.RemoveAt(listBox_levels.SelectedIndex);
            ReloadLevelList();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFileDialog = new OpenFileDialog();
            oFileDialog.ShowDialog();
            oMapList.Add(new Map("DEFAULT_MAP" + oMapList.Count+1, "Default name", oFileDialog.FileName));            
            ReloadMapList();
        }

        private void tSBtn_mapsRemove_Click(object sender, EventArgs e)
        {
            if (listBox_maps.SelectedIndex == -1)
                return;
            oMapList.RemoveAt(listBox_maps.SelectedIndex);
            ReloadMapList();
        }

        private void tSBtn_ItemsAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFileDialog = new OpenFileDialog();
            oFileDialog.ShowDialog();
            oItemList.Add(new Item("DEFAULT_ITEM"+oItemList.Count+1,oFileDialog.FileName));            
            ReloadItemList();
        }

        private void tSBtn_itemsRemove_Click(object sender, EventArgs e)
        {
            if (listBox_items.SelectedIndex == -1)
                return;
            oItemList.RemoveAt(listBox_items.SelectedIndex);
            ReloadItemList();
            RedrawLevel();
        }

        private void listBox_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_items.SelectedIndex == -1)
            {
                propertyGrid1.SelectedObject = null;
                itemImg = null;
            }
            else
            {
                propertyGrid1.SelectedObject = oItemList.ElementAt<Item>(listBox_items.SelectedIndex);
                try
                {
                    itemImg = Image.FromFile(oItemList.ElementAt<Item>(listBox_items.SelectedIndex).assetName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void listBox_levels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_levels.SelectedIndex == -1)
                propertyGrid1.SelectedObject = null;
            else
                propertyGrid1.SelectedObject = oLevelList.ElementAt<Level>(listBox_levels.SelectedIndex);
        }

        private void listBox_maps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_maps.SelectedIndex == -1)
            {
                propertyGrid1.SelectedObject = null;
                gfx.Clear(Color.White);
                pBox_map.Size = new Size(0, 0);
            }
            else
            {                
                propertyGrid1.SelectedObject = oMapList.ElementAt<Map>(listBox_maps.SelectedIndex);
                //propertyGrid_itemsInfo.SelectedObjects = oMapList.ElementAt<Map>(listBox_maps.SelectedIndex).allowedItems.;
                pBox_map.Size = new Size(MOTOROLA_DROID_WIDTH, MOTOROLA_DROID_HEIGHT);
                RedrawLevel();
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int temp;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    temp = listBox_levels.SelectedIndex;
                    ReloadLevelList();
                    listBox_levels.SelectedIndex = temp;
                    break;
                case 1:
                    temp = listBox_maps.SelectedIndex;
                    ReloadMapList();
                    listBox_maps.SelectedIndex = temp;
                    break;
                case 2:
                    temp = listBox_items.SelectedIndex;
                    ReloadItemList();
                    listBox_items.SelectedIndex = temp;
                    break;
            }
        }

        private void pBox_map_Click(object sender, EventArgs e)
        {
            if (itemImg != null)
            {
                if (listBox_maps.SelectedIndex == -1)
                    MessageBox.Show("First select map.");
                else
                {
                    Point position = Cursor.Position;
                    position = pBox_map.PointToClient(position);
                    position.X -= itemImg.Size.Width / 2;
                    position.Y -= itemImg.Size.Height / 2;

                    oMapList.ElementAt<Map>(listBox_maps.SelectedIndex).AddItem(oItemList.ElementAt<Item>(listBox_items.SelectedIndex).id, new Rectangle(position, itemImg.Size));                                                           
                    RedrawLevel();
                }
            }
        }

        private void pBox_map_MouseEnter(object sender, EventArgs e)
        {
            if (itemImg != null)
            {
                Bitmap temp = new Bitmap(itemImg);
                this.Cursor = CursorConverter.CreateCursor(temp, temp.Width / 2, temp.Height / 2);
                temp.Dispose();
            }
        }

        private void pBox_map_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private bool RedrawLevel()
        {
            Bitmap oTempBitmap;
            Image oTempImage;

            gfx.Clear(Color.White);

            try
            {
                //background
                oTempImage = Image.FromFile(oMapList.ElementAt<Map>(listBox_maps.SelectedIndex).background);
                oTempBitmap = new Bitmap(oTempImage);
                gfx.DrawImage(oTempBitmap, 0, 0, MOTOROLA_DROID_WIDTH, MOTOROLA_DROID_HEIGHT);
                oTempImage.Dispose();
                oTempBitmap.Dispose();

                //items
                foreach (ItemInfo ii in oMapList.ElementAt<Map>(listBox_maps.SelectedIndex).allowedItems)
                {
                    oTempImage = EdytorekJakRozporek.Properties.Resources.none;
                    foreach (Item item in oItemList)
                    {
                        if (item.id == ii.id)
                        {
                            oTempImage = Image.FromFile(item.assetName);
                            break;
                        }
                    }
                    oTempBitmap = new Bitmap(oTempImage);
                    gfx.DrawImage(oTempBitmap, ii.placement);
                    oTempBitmap.Dispose();
                    oTempImage.Dispose();
                }
                pBox_map.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RedrawLevel() Error: " + ex.Message);
                return false;
            }
            return true;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JEBAJEJE");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "pak";
            saveFileDialog.Filter = "PAK File|*.pak";
            saveFileDialog.Title = "Save an Level Pack File";
            saveFileDialog.ShowDialog();
            if(saveFileDialog.FileName != "")
            {
                XmlRootAttribute oRootAttr = new XmlRootAttribute();
                oRootAttr.ElementName = "Levels";
                oRootAttr.IsNullable = true;
                XmlSerializer oSerializer = new XmlSerializer(typeof(List<Level>), oRootAttr);
                StreamWriter oStreamWriter = null;

                try
                {
                    oStreamWriter = new StreamWriter("level.xml");
                    oSerializer.Serialize(oStreamWriter, oLevelList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save error: " + ex.Message);
                    return;
                }
                finally
                {
                    if (null != oStreamWriter)
                    {
                        oStreamWriter.Dispose();
                    }
                }

                oRootAttr.ElementName = "Maps";
                oRootAttr.IsNullable = true;
                oSerializer = new XmlSerializer(typeof(List<Map>), oRootAttr);
                oStreamWriter = null;

                try
                {
                    oStreamWriter = new StreamWriter("map.xml");
                    oSerializer.Serialize(oStreamWriter, oMapList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save error: " + ex.Message);
                    return;
                }
                finally
                {
                    if (null != oStreamWriter)
                    {
                        oStreamWriter.Dispose();
                    }
                }

                oRootAttr.ElementName = "Items";
                oRootAttr.IsNullable = true;
                oSerializer = new XmlSerializer(typeof(List<Item>), oRootAttr);
                oStreamWriter = null;

                try
                {
                    oStreamWriter = new StreamWriter("item.xml");
                    oSerializer.Serialize(oStreamWriter, oItemList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save error: " + ex.Message);
                    return;
                }
                finally
                {
                    if (null != oStreamWriter)
                    {
                        oStreamWriter.Dispose();
                    }
                }

                try
                {
                
                    int i = 0;
                    string directory;
                    do
                    {
                        directory = "./tmplevel" + i++ + "/";
                    }
                    while (Directory.Exists(directory));
                   
                    Directory.CreateDirectory(directory);
                                
                    File.Copy("level.xml", directory + "level.xml");
                    File.Copy("item.xml", directory + "item.xml");
                    File.Copy("map.xml", directory + "map.xml");

                    foreach (Map map in oMapList)
                    {
                        File.Copy(map.background, directory + map.asset);
                    }
                    foreach (Item item in oItemList)
                    {
                        File.Copy(item.assetName, directory + item.asset);
                    }
                
                    DirectoryInfo di = new DirectoryInfo(directory);
                    ZipFile zip = new ZipFile();
                    zip.CompressionLevel = CompressionLevel.None;
                    foreach (FileInfo fi in di.GetFiles()) 
                    {                    
                        zip.AddFile(fi.FullName, "./");                   
                    }                                
                    zip.Save(saveFileDialog.FileName);
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        File.Delete(fi.FullName);
                    }
                    Directory.Delete(directory);                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save error: " + ex.Message);
                    return;
                }
                MessageBox.Show("Successfully save level pack.\nPath: " + saveFileDialog.FileName);

            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Level pack|*.PAK";
            openFileDialog.DefaultExt = "PAK";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                if (oItemList.Count > 0 || oLevelList.Count > 0 || oMapList.Count > 0)
                {
                    if (MessageBox.Show("There is some of data at your actual workplace.\n Do you really want to open file and lose your unsaved progress?", "Open file confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                string tempPath = Path.GetTempPath();

                oItemList.Clear(); oLevelList.Clear(); oMapList.Clear();
                gfx.Clear(Color.White);

                try
                {
                    int i = 0;
                    string directory;                        
                    ZipFile zip = new ZipFile(openFileDialog.FileName);
                    do
                    {
                        directory = tempPath + openFileDialog.SafeFileName + i++;
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
                        Map map = new Map(oMap.GetAttribute("Id",""),
                                            oMap.SelectSingleNode("Name").Value,
                                            directory + oMap.SelectSingleNode("Asset"));
                                
                        XPathNavigator    oXPathItemNavigator = oMap.CreateNavigator();
                        XPathNodeIterator oItemsNodesIterator = oMap.Select("Item");
                        foreach (XPathNavigator oMapItem in oItemsNodesIterator)
                        {
                            ItemInfo ii;
                            ii.id = oMapItem.GetAttribute("Id","");
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
                        Item item = new Item(oItem.GetAttribute("Id",""),
                                                directory + oItem.SelectSingleNode("Asset").Value,
                                                oItem.SelectSingleNode("Name").Value);
                        oItemList.Add(item);
                    }

                    ReloadItemList();
                    ReloadLevelList();
                    ReloadMapList();
                    MessageBox.Show("Successfully loaded level pack.\nPath: " + openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Openfile error: " + ex.Message);
                }
            }
                     
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl2.TabPages[0].Text = tabControl1.SelectedTab.Text.TrimEnd('s') + " info";            
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
