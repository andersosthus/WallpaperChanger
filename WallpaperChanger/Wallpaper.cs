using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WallpaperChanger
{
    // From http://stackoverflow.com/questions/1061678/change-desktop-wallpaper-using-code-in-net/1061682#1061682
    public static class Wallpaper
    {
        private const int SetDesktopWallpaper = 20;
        private const int UpdateIniFile = 0x01;
        private const int SendWinIniChange = 0x02;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void SetRandomWallpaperFromPath(FileInfo file, Style style)
        {
            if (file == null) return;

            SystemParametersInfo(SetDesktopWallpaper, 0, file.ToString(), UpdateIniFile | SendWinIniChange);
            var key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);

            if (key == null) return;

            switch (style)
            {
                case Style.Stretch:
                    key.SetValue(@"WallpaperStyle", "2");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case Style.Center:
                    key.SetValue(@"WallpaperStyle", "1");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case Style.Tile:
                    key.SetValue(@"WallpaperStyle", "1");
                    key.SetValue(@"TileWallpaper", "1");
                    break;
                case Style.NoChange:
                    break;
            }

            key.Close();
        }
    }
}