using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperChanger.Properties;

namespace WallpaperChanger
{
    public interface IWallpaperService
    {
        Task Start(CancellationToken token, Action<string> callback);
        void NextWallpaper(Action<string> callback);
    }

    public class WallpaperService : IWallpaperService
    {
        public async Task Start(CancellationToken token, Action<string> callback)
        {
            var timeout = Settings.Default.ChangeInterval;

            do
            {
                SetNextWallpaper(callback);
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(timeout), token);
                }
                catch (TaskCanceledException ex)
                {
                }

            } while (!token.IsCancellationRequested);
        }

        public void NextWallpaper(Action<string> callback)
        {
            SetNextWallpaper(callback);
        }
        
        private static void SetNextWallpaper(Action<string> callback)
        {
            var wallpaperPath = Settings.Default.WallpaperPath;
            var tileType = (Style) Settings.Default.SelectedStyle;

            var wallpaper = Wallpaper.SetRandomWallpaperFromPath(wallpaperPath, tileType);
            callback(wallpaper);
        }
    }
}