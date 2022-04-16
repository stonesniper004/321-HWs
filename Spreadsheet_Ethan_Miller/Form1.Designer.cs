namespace Spreadsheet_Ethan_Miller
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.DemoButton = new System.Windows.Forms.Button();
            this.File = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit_Drop_Down = new System.Windows.Forms.ToolStripDropDownButton();
            this.undo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.redo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Cell_Drop_Down = new System.Windows.Forms.ToolStripDropDownButton();
            this.change_Back_ground_Color_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.color_Dialog_User = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.File.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(-2, 43);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 62;
            this.dataGrid.Size = new System.Drawing.Size(1197, 423);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
            // 
            // DemoButton
            // 
            this.DemoButton.Location = new System.Drawing.Point(-2, 475);
            this.DemoButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DemoButton.Name = "DemoButton";
            this.DemoButton.Size = new System.Drawing.Size(1197, 31);
            this.DemoButton.TabIndex = 1;
            this.DemoButton.Text = "Demo";
            this.DemoButton.UseVisualStyleBackColor = true;
            this.DemoButton.Click += new System.EventHandler(this.DemoButton_Click);
            // 
            // File
            // 
            this.File.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.File.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.File.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.Edit_Drop_Down,
            this.Cell_Drop_Down});
            this.File.Location = new System.Drawing.Point(0, 0);
            this.File.Name = "File";
            this.File.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.File.Size = new System.Drawing.Size(1200, 34);
            this.File.TabIndex = 2;
            this.File.Text = "File";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(56, 29);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // Edit_Drop_Down
            // 
            this.Edit_Drop_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Edit_Drop_Down.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undo_Button,
            this.redo_Button});
            this.Edit_Drop_Down.Image = ((System.Drawing.Image)(resources.GetObject("Edit_Drop_Down.Image")));
            this.Edit_Drop_Down.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_Drop_Down.Name = "Edit_Drop_Down";
            this.Edit_Drop_Down.Size = new System.Drawing.Size(60, 29);
            this.Edit_Drop_Down.Text = "Edit";
            // 
            // undo_Button
            // 
            this.undo_Button.Name = "undo_Button";
            this.undo_Button.Size = new System.Drawing.Size(158, 34);
            this.undo_Button.Text = "Undo";
            this.undo_Button.Click += new System.EventHandler(this.Undo_Button_Click);
            // 
            // redo_Button
            // 
            this.redo_Button.Name = "redo_Button";
            this.redo_Button.Size = new System.Drawing.Size(158, 34);
            this.redo_Button.Text = "Redo";
            this.redo_Button.Click += new System.EventHandler(this.Redo_Button_Click);
            // 
            // Cell_Drop_Down
            // 
            this.Cell_Drop_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Cell_Drop_Down.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.change_Back_ground_Color_Button});
            this.Cell_Drop_Down.Image = ((System.Drawing.Image)(resources.GetObject("Cell_Drop_Down.Image")));
            this.Cell_Drop_Down.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cell_Drop_Down.Name = "Cell_Drop_Down";
            this.Cell_Drop_Down.Size = new System.Drawing.Size(58, 29);
            this.Cell_Drop_Down.Text = "Cell";
            // 
            // change_Back_ground_Color_Button
            // 
            this.change_Back_ground_Color_Button.Name = "change_Back_ground_Color_Button";
            this.change_Back_ground_Color_Button.Size = new System.Drawing.Size(322, 34);
            this.change_Back_ground_Color_Button.Text = "Change Background Color";
            this.change_Back_ground_Color_Button.Click += new System.EventHandler(this.Change_Back_ground_Color_Button_Click);
            // 
            // Form1
            // 
            this.AccessibleDescription = "";
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 506);
            this.Controls.Add(this.File);
            this.Controls.Add(this.DemoButton);
            this.Controls.Add(this.dataGrid);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Spreedsheet";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.File.ResumeLayout(false);
            this.File.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button DemoButton;
        private System.Windows.Forms.ToolStrip File;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripDropDownButton Edit_Drop_Down;
        private System.Windows.Forms.ToolStripMenuItem undo_Button;
        private System.Windows.Forms.ToolStripMenuItem redo_Button;
        private System.Windows.Forms.ToolStripDropDownButton Cell_Drop_Down;
        private System.Windows.Forms.ToolStripMenuItem change_Back_ground_Color_Button;
        private System.Windows.Forms.ColorDialog color_Dialog_User;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    }
}

