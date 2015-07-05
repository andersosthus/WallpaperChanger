namespace WallpaperChanger
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.NotificationIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Exit = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.WallpaperFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChangeInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StyleSelector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // NotificationIcon
            // 
            this.NotificationIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotificationIcon.Icon")));
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(12, 70);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 0;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(289, 70);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(370, 70);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 2;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wallpaper Folder";
            // 
            // WallpaperFolder
            // 
            this.WallpaperFolder.Location = new System.Drawing.Point(164, 10);
            this.WallpaperFolder.Name = "WallpaperFolder";
            this.WallpaperFolder.Size = new System.Drawing.Size(281, 20);
            this.WallpaperFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Change interval (Seconds)";
            // 
            // ChangeInterval
            // 
            this.ChangeInterval.Location = new System.Drawing.Point(164, 43);
            this.ChangeInterval.Name = "ChangeInterval";
            this.ChangeInterval.Size = new System.Drawing.Size(41, 20);
            this.ChangeInterval.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Style";
            // 
            // StyleSelector
            // 
            this.StyleSelector.FormattingEnabled = true;
            this.StyleSelector.Location = new System.Drawing.Point(253, 43);
            this.StyleSelector.Name = "StyleSelector";
            this.StyleSelector.Size = new System.Drawing.Size(192, 21);
            this.StyleSelector.TabIndex = 8;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 104);
            this.ControlBox = false;
            this.Controls.Add(this.StyleSelector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChangeInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WallpaperFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wallpaper Changer";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotificationIcon;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WallpaperFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ChangeInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox StyleSelector;
    }
}

