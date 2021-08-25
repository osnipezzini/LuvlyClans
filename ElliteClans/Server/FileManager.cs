namespace ElliteClans.Server
{
    class FileManager
    {
        public static string bpxConfigPath = BepInEx.Paths.BepInExConfigPath;
        public static string lcConfigPath = System.IO.Path.Combine(bpxConfigPath, Startup.PluginGUID + ".cfg");
    }
}
