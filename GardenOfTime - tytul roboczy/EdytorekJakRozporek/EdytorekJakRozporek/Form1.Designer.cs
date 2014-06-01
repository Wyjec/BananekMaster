namespace EdytorekJakRozporek
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_mapa = new System.Windows.Forms.Panel();
            this.pBox_map = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.propertyGrid_itemsInfo = new System.Windows.Forms.PropertyGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBox_levels = new System.Windows.Forms.ListBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tSBtn_levelsAdd = new System.Windows.Forms.ToolStripButton();
            this.tSBtn_levelsRemove = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox_maps = new System.Windows.Forms.ListBox();
            this.tSBtn_mapsAdd = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tSBtn_mapsRemove = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listBox_items = new System.Windows.Forms.ListBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.tSBtn_ItemsAdd = new System.Windows.Forms.ToolStripButton();
            this.tSBtn_itemsRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_mapa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_map)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tSBtn_mapsAdd.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_mapa
            // 
            this.panel_mapa.AutoScroll = true;
            this.panel_mapa.Controls.Add(this.pBox_map);
            this.panel_mapa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_mapa.Location = new System.Drawing.Point(0, 0);
            this.panel_mapa.Name = "panel_mapa";
            this.panel_mapa.Size = new System.Drawing.Size(869, 456);
            this.panel_mapa.TabIndex = 0;
            // 
            // pBox_map
            // 
            this.pBox_map.BackColor = System.Drawing.Color.White;
            this.pBox_map.Location = new System.Drawing.Point(9, 9);
            this.pBox_map.Name = "pBox_map";
            this.pBox_map.Size = new System.Drawing.Size(0, 0);
            this.pBox_map.TabIndex = 0;
            this.pBox_map.TabStop = false;
            this.pBox_map.Click += new System.EventHandler(this.pBox_map_Click);
            this.pBox_map.MouseEnter += new System.EventHandler(this.pBox_map_MouseEnter);
            this.pBox_map.MouseLeave += new System.EventHandler(this.pBox_map_MouseLeave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1061, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Open";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tabControl2);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(869, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 456);
            this.panel1.TabIndex = 2;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 248);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(192, 208);
            this.tabControl2.TabIndex = 1;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.propertyGrid1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(184, 182);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Level info";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(178, 176);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.propertyGrid_itemsInfo);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(184, 182);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Allowed Items";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // propertyGrid_itemsInfo
            // 
            this.propertyGrid_itemsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid_itemsInfo.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid_itemsInfo.Name = "propertyGrid_itemsInfo";
            this.propertyGrid_itemsInfo.Size = new System.Drawing.Size(178, 176);
            this.propertyGrid_itemsInfo.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(192, 248);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox_levels);
            this.tabPage1.Controls.Add(this.toolStrip3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(184, 222);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Levels";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox_levels
            // 
            this.listBox_levels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_levels.FormattingEnabled = true;
            this.listBox_levels.Location = new System.Drawing.Point(3, 3);
            this.listBox_levels.Name = "listBox_levels";
            this.listBox_levels.Size = new System.Drawing.Size(178, 193);
            this.listBox_levels.TabIndex = 2;
            this.listBox_levels.SelectedIndexChanged += new System.EventHandler(this.listBox_levels_SelectedIndexChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSBtn_levelsAdd,
            this.tSBtn_levelsRemove});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip3.Location = new System.Drawing.Point(3, 196);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(178, 23);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tSBtn_levelsAdd
            // 
            this.tSBtn_levelsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBtn_levelsAdd.Image = global::EdytorekJakRozporek.Properties.Resources.add;
            this.tSBtn_levelsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_levelsAdd.Name = "tSBtn_levelsAdd";
            this.tSBtn_levelsAdd.Size = new System.Drawing.Size(23, 20);
            this.tSBtn_levelsAdd.Text = "toolStripButton1";
            this.tSBtn_levelsAdd.Click += new System.EventHandler(this.tSBtn_levelsAdd_Click);
            // 
            // tSBtn_levelsRemove
            // 
            this.tSBtn_levelsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBtn_levelsRemove.Image = global::EdytorekJakRozporek.Properties.Resources.remove;
            this.tSBtn_levelsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_levelsRemove.Name = "tSBtn_levelsRemove";
            this.tSBtn_levelsRemove.Size = new System.Drawing.Size(23, 20);
            this.tSBtn_levelsRemove.Text = "toolStripButton1";
            this.tSBtn_levelsRemove.Click += new System.EventHandler(this.tSBtn_levelsRemove_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBox_maps);
            this.tabPage2.Controls.Add(this.tSBtn_mapsAdd);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(184, 222);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Maps";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox_maps
            // 
            this.listBox_maps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_maps.FormattingEnabled = true;
            this.listBox_maps.Location = new System.Drawing.Point(3, 3);
            this.listBox_maps.Name = "listBox_maps";
            this.listBox_maps.Size = new System.Drawing.Size(178, 193);
            this.listBox_maps.TabIndex = 3;
            this.listBox_maps.SelectedIndexChanged += new System.EventHandler(this.listBox_maps_SelectedIndexChanged);
            // 
            // tSBtn_mapsAdd
            // 
            this.tSBtn_mapsAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tSBtn_mapsAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.tSBtn_mapsRemove});
            this.tSBtn_mapsAdd.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tSBtn_mapsAdd.Location = new System.Drawing.Point(3, 196);
            this.tSBtn_mapsAdd.Name = "tSBtn_mapsAdd";
            this.tSBtn_mapsAdd.Size = new System.Drawing.Size(178, 23);
            this.tSBtn_mapsAdd.TabIndex = 1;
            this.tSBtn_mapsAdd.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::EdytorekJakRozporek.Properties.Resources.add;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // tSBtn_mapsRemove
            // 
            this.tSBtn_mapsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBtn_mapsRemove.Image = global::EdytorekJakRozporek.Properties.Resources.remove;
            this.tSBtn_mapsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_mapsRemove.Name = "tSBtn_mapsRemove";
            this.tSBtn_mapsRemove.Size = new System.Drawing.Size(23, 20);
            this.tSBtn_mapsRemove.Text = "toolStripButton1";
            this.tSBtn_mapsRemove.Click += new System.EventHandler(this.tSBtn_mapsRemove_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBox_items);
            this.tabPage3.Controls.Add(this.toolStrip4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(184, 222);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Items";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listBox_items
            // 
            this.listBox_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_items.FormattingEnabled = true;
            this.listBox_items.Location = new System.Drawing.Point(0, 0);
            this.listBox_items.Name = "listBox_items";
            this.listBox_items.Size = new System.Drawing.Size(184, 222);
            this.listBox_items.TabIndex = 3;
            this.listBox_items.SelectedIndexChanged += new System.EventHandler(this.listBox_items_SelectedIndexChanged);
            // 
            // toolStrip4
            // 
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSBtn_ItemsAdd,
            this.tSBtn_itemsRemove});
            this.toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip4.Location = new System.Drawing.Point(0, 199);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(184, 23);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            this.toolStrip4.Visible = false;
            // 
            // tSBtn_ItemsAdd
            // 
            this.tSBtn_ItemsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBtn_ItemsAdd.Image = global::EdytorekJakRozporek.Properties.Resources.add;
            this.tSBtn_ItemsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_ItemsAdd.Name = "tSBtn_ItemsAdd";
            this.tSBtn_ItemsAdd.Size = new System.Drawing.Size(23, 20);
            this.tSBtn_ItemsAdd.Text = "toolStripButton2";
            this.tSBtn_ItemsAdd.Click += new System.EventHandler(this.tSBtn_ItemsAdd_Click);
            // 
            // tSBtn_itemsRemove
            // 
            this.tSBtn_itemsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBtn_itemsRemove.Image = global::EdytorekJakRozporek.Properties.Resources.remove;
            this.tSBtn_itemsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_itemsRemove.Name = "tSBtn_itemsRemove";
            this.tSBtn_itemsRemove.Size = new System.Drawing.Size(23, 20);
            this.tSBtn_itemsRemove.Text = "toolStripButton1";
            this.tSBtn_itemsRemove.Click += new System.EventHandler(this.tSBtn_itemsRemove_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Location = new System.Drawing.Point(0, 480);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1061, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel_mapa);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1061, 456);
            this.panel2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 505);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Level Editor";
            this.panel_mapa.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBox_map)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tSBtn_mapsAdd.ResumeLayout(false);
            this.tSBtn_mapsAdd.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_mapa;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.PictureBox pBox_map;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStrip tSBtn_mapsAdd;
        private System.Windows.Forms.ToolStripButton tSBtn_levelsAdd;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton tSBtn_ItemsAdd;
        private System.Windows.Forms.ToolStripButton tSBtn_levelsRemove;
        private System.Windows.Forms.ToolStripButton tSBtn_mapsRemove;
        private System.Windows.Forms.ToolStripButton tSBtn_itemsRemove;
        private System.Windows.Forms.ListBox listBox_levels;
        private System.Windows.Forms.ListBox listBox_maps;
        private System.Windows.Forms.ListBox listBox_items;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.PropertyGrid propertyGrid_itemsInfo;
    }
}

