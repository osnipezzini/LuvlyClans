using ElliteClans.Server.Types;
using System.IO;
using Log = Jotunn.Logger;

namespace ElliteClans.Server
{
    public class ServerManager
    {
        public static void LoadClans()
        {
            Startup.DatabaseServer = new DB(
                Path.Combine(Startup.ConfigPath, Startup.DatabasePath), 
                Path.Combine(Startup.ConfigPath, Startup.BackupDatabasePath)
            );

            Startup.ClansDatabase = Startup.DatabaseServer != null ? Startup.DatabaseServer.Read() : new Clans();
            Startup.ClansServer = Startup.ClansDatabase;

            Log.LogInfo($"Loaded {Startup.ClansDatabase.m_clans.Length} clans");
        }
    }
}
