using LuvlyClans.Server.Types;
using System.IO;
using Log = Jotunn.Logger;

namespace LuvlyClans.Server
{
    public class ServerManager
    {
        public static void LoadClans()
        {
            Startup.m_db_server = new DB(
                Path.Combine(Startup.m_path_config, Startup.m_path_db), 
                Path.Combine(Startup.m_path_config, Startup.m_path_db_backup)
            );

            Startup.m_clans_db = Startup.m_db_server != null ? Startup.m_db_server.Read() : new Clans();
            Startup.m_clans_server = Startup.m_clans_db;

            Log.LogInfo($"Loaded {Startup.m_clans_db.m_clans.Length} clans");
        }
    }
}
