using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperChanger.Properties;

namespace WallpaperChanger
{
    public interface IWallpaperService
    {
        Task Start(CancellationToken token, Action callback);
        void NextWallpaper(Action callback);
        string CurrentWallpaper { get; }
    }

    public class WallpaperService : IWallpaperService
    {
        public async Task Start(CancellationToken token, Action callback)
        {
            var timeout = Settings.Default.ChangeInterval;

            do
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(timeout), token);
                    SetNextWallpaper(callback);
                }
                catch (TaskCanceledException ex)
                {
                }

            } while (!token.IsCancellationRequested);
        }

        public void NextWallpaper(Action callback)
        {
            SetNextWallpaper(callback);
        }

        public string CurrentWallpaper { get; private set; }

        private void SetNextWallpaper(Action callback)
        {
            var wallpaperPath = Settings.Default.WallpaperPath;
            var tileType = (Style) Settings.Default.SelectedStyle;

            if (string.IsNullOrEmpty(wallpaperPath))
            {
                CurrentWallpaper = string.Empty;
                return;
            }

            var wallpaper = Wallpaper.SetRandomWallpaperFromPath(wallpaperPath, tileType);
            CurrentWallpaper = wallpaper;
            callback();
        }
    }
}