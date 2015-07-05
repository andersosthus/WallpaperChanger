﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WallpaperChanger
{
    public static class Wallpaper
    {
        private const int SetDesktopWallpaper = 20;
        private const int UpdateIniFile = 0x01;
        private const int SendWinIniChange = 0x02;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static string SetRandomWallpaperFromPath(string path, Style style)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            if (!Directory.Exists(path))
                return null;

            var files = Directory.GetFiles(path);
            var rand = new Random();
            var num = rand.Next(0, files.Length - 1);

            var wallpaperFullPath = files[num];
            var file = new FileInfo(wallpaperFullPath);

            SystemParametersInfo(SetDesktopWallpaper, 0, wallpaperFullPath, UpdateIniFile | SendWinIniChange);
            var key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            if (key == null)
                return null;

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

            return (file.Name.Length >= 64)
                ? file.Name.Substring(0,63)
                : file.Name;
        }
    }
}