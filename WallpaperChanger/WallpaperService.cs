using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        void ResetCache();
        string CurrentWallpaper { get; }
    }

    public class WallpaperService : IWallpaperService
    {
        private readonly List<string> _wallpaperFiles = new List<string>();
        private readonly Random _rand = new Random();
        private readonly ConcurrentDeck<string> _history = new ConcurrentDeck<string>(20); 

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

        public void ResetCache()
        {
            _wallpaperFiles.Clear();
        }

        public string CurrentWallpaper { get; private set; }

        private void SetNextWallpaper(Action callback)
        {
            if (!_wallpaperFiles.Any())
            {
                var wallpaperPath = Settings.Default.WallpaperPath;
                if (!Directory.Exists(wallpaperPath)) return;
                var files = Directory.GetFiles(wallpaperPath, "*", SearchOption.AllDirectories).ToList();
                _wallpaperFiles.AddRange(files);
            }

            var tileType = (Style) Settings.Default.SelectedStyle;
            
            var file = GetNewWallpaperFile();

            Wallpaper.SetRandomWallpaperFromPath(file, tileType);
            CurrentWallpaper = file.ToString();
            
            _history.Push(file.ToString());

            callback();
        }

        private FileInfo GetNewWallpaperFile()
        {
            string wallpaperFullPath;

            do
            {
                var num = _rand.Next(0, _wallpaperFiles.Count - 1);
                wallpaperFullPath = _wallpaperFiles[num];

            } while (_history.Contains(wallpaperFullPath));
            
            var file = new FileInfo(wallpaperFullPath);
            return file;
        }
    }
}