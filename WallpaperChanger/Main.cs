using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperChanger.Properties;

namespace WallpaperChanger
{
    public partial class Main : Form
    {
        private readonly WallpaperService _wallpaper;
        private ContextMenu _notificationMenu;
        private List<MenuItem> _notificationMenuItems;
        private CancellationTokenSource _cts;

        public Main()
        {
            InitializeComponent();

            ConfigureContextMenu();

            _wallpaper = new WallpaperService();
        }

        private void ConfigureContextMenu()
        {
            var showItem = new MenuItem
            {
                Index = 0,
                Text = "&Settings"
            };
            showItem.Click += ShowSettings;

            var exitItem = new MenuItem
            {
                Index = 1,
                Text = "E&xit"
            };
            exitItem.Click += Exit_Click;

            _notificationMenuItems = new List<MenuItem> { showItem, exitItem };

            _notificationMenu = new ContextMenu();
            _notificationMenu.MenuItems.AddRange(_notificationMenuItems.ToArray());
        }

        internal void DisplayNotification(string name)
        {
            try
            {
                NotificationIcon.BalloonTipText = $"Wallpaper changed to {name}";
                NotificationIcon.ShowBalloonTip(1000);
            }
            catch (Exception ex)
            {
            }
        }

        internal void SetNotificationTooltip(string wallpaper)
        {
            NotificationIcon.Text = wallpaper;
        }

        private void ConfigureNotificationIcon()
        {
            NotificationIcon.Icon = Resource.Main;
            NotificationIcon.Visible = true;
            NotificationIcon.ContextMenu = _notificationMenu;
            NotificationIcon.Click += NextWallpaper;
        }

        private void NextWallpaper(object sender, EventArgs e)
        {
            _wallpaper.NextWallpaper(SetNotificationTooltip);
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            LoadSettings();
            WindowState = FormWindowState.Normal;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Icon = Resource.Main;

            LoadSettings();
            ConfigureNotificationIcon();

            _cts = new CancellationTokenSource();

            var task = Task.Run(async () => await _wallpaper.Start(_cts.Token, SetNotificationTooltip));
        }

        private void LoadSettings()
        {
            var names = Enum.GetNames(typeof (Style));
            StyleSelector.Items.AddRange(names);
            var selectedStyle = Settings.Default.SelectedStyle;
            StyleSelector.SelectedIndex = selectedStyle;

            ChangeInterval.Text = Settings.Default.ChangeInterval.ToString();
            WallpaperFolder.Text = Settings.Default.WallpaperPath;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            NotificationIcon.Visible = false;
            NotificationIcon.Dispose();

            _cts.Cancel();

            Application.Exit();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!SaveSettings()) return;

            WindowState = FormWindowState.Minimized;

            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var task = Task.Run(async () => await _wallpaper.Start(_cts.Token, SetNotificationTooltip));
        }

        private bool SaveSettings()
        {
            Settings.Default.SelectedStyle = StyleSelector.SelectedIndex;

            int changeInterval;
            if (!int.TryParse(ChangeInterval.Text, out changeInterval))
            {
                MessageBox.Show("Invalid value for Change Interval!");
                return false;
            }
            Settings.Default.ChangeInterval = changeInterval;

            if (!Directory.Exists(WallpaperFolder.Text))
            {
                MessageBox.Show("Wallpaper folder does not exist!");
                return false;
            }
            Settings.Default.WallpaperPath = WallpaperFolder.Text;

            Settings.Default.Save();
            return true;
        }
    }
}