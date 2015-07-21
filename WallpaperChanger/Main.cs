using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

            var deleteItem = new MenuItem
            {
                Index = 1,
                Text =  "Delete current image"
            };
            deleteItem.Click += DeleteImage;

            var exitItem = new MenuItem
            {
                Index = 2,
                Text = "E&xit"
            };
            exitItem.Click += Exit_Click;

            _notificationMenuItems = new List<MenuItem> { showItem, deleteItem, exitItem };

            _notificationMenu = new ContextMenu();
            _notificationMenu.MenuItems.AddRange(_notificationMenuItems.ToArray());
        }

        private void DeleteImage(object sender, EventArgs e)
        {
            if (_wallpaper == null)
                return;

            var file = new FileInfo(_wallpaper.CurrentWallpaper);

            SwitchToNextWallpaper();
            File.Delete(file.ToString());
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

        internal void SetNotificationTooltip()
        {
            if (_wallpaper == null)
                return;

            var file = new FileInfo(_wallpaper.CurrentWallpaper);
            
            NotificationIcon.Text = (file.Name.Length >= 64)
                ? file.Name.Substring(0, 63)
                : file.Name;
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
            var args = e as MouseEventArgs;
            if (args?.Button == MouseButtons.Left)
            {
                SwitchToNextWallpaper();
            }
        }

        private void SwitchToNextWallpaper()
        {
            _wallpaper.NextWallpaper(SetNotificationTooltip);
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var task = Task.Run(async () => await _wallpaper.Start(_cts.Token, SetNotificationTooltip));
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            LoadSettings();
            Show();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Icon = Resource.Main;

            LoadSettings();
            ConfigureNotificationIcon();
            Hide();

            _cts = new CancellationTokenSource();

            var task = Task.Run(async () => await _wallpaper.Start(_cts.Token, SetNotificationTooltip));
        }

        private void LoadSettings()
        {
            var names = Enum.GetNames(typeof(Style));
            StyleSelector.Items.AddRange(names);
            var selectedStyle = Settings.Default.SelectedStyle;
            StyleSelector.SelectedIndex = selectedStyle;

            RunOnStartup.Checked = RegistryHelper.IsRunOnStartupEnabled();

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
            Hide();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!SaveSettings()) return;

            Hide();

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

            if(RunOnStartup.Checked)
                RegistryHelper.EnableRunOnStartup(Application.ExecutablePath);
            else
                RegistryHelper.DisableRunOnStartup();

            return true;
        }
    }
}