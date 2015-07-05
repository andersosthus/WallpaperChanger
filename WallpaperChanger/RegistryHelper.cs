using Microsoft.Win32;

namespace WallpaperChanger
{
    public static class RegistryHelper
    {
        private const string ApplicationName = "Wallpaper Changer";
        private const string RunOnStartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static void EnableRunOnStartup(string executablePath)
        {
            var currentKey = Registry.CurrentUser.OpenSubKey(RunOnStartupKey, true);
            currentKey?.SetValue(ApplicationName, executablePath);
        }

        public static void DisableRunOnStartup()
        {
            var currentKey = Registry.CurrentUser.OpenSubKey(RunOnStartupKey, true);

            if(IsRunOnStartupEnabled())
                currentKey?.DeleteValue(ApplicationName);
        }

        public static bool IsRunOnStartupEnabled()
        {
            var currentKey = Registry.CurrentUser.OpenSubKey(RunOnStartupKey, true);
            var val = currentKey?.GetValue(ApplicationName);

            return (val != null);
        }
    }
}