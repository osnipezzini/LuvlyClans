using ElliteClans.Server.Types;
using System.IO;
using StackExchange.Redis;
using JSON = SimpleJson.SimpleJson;
using Log = Jotunn.Logger;

namespace ElliteClans.Server
{
    public class DB
    {
        public static string DatabasePath { get; private set; }
        public static string BackupDatabasePath { get; private set; }

        public static bool HasDb => Exists();
        public static bool HasBackupDb => Exists(BackupDatabasePath);

        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");

        public DB(string path, string path_backup = null)
        {
            DatabasePath = path;
            BackupDatabasePath = path_backup == null ? "old." + path : path_backup;
        }

        public static bool Exists(string path = null)
        {
            return File.Exists(path == null ? DatabasePath : path);
        }

        public static void ReadRedis()
        {
            IDatabase db = redis.GetDatabase();
            string clans = db.StringGet("valheim_clans");

            if (clans != null)
            {
                Log.LogInfo(clans);
            }
        }

        public Clans Read()
        {
            if (HasDb)
            {
                string data = File.ReadAllText(DatabasePath);

                if (data != null)
                {
                    return JSON.DeserializeObject<Clans>(data);
                }

                return new Clans();
            }

            return new Clans();
        }

        public static void Write(string json)
        {
            if (File.Exists(BackupDatabasePath))
            {
                File.Delete(BackupDatabasePath);
            }

            File.Copy(DatabasePath, BackupDatabasePath);
            File.WriteAllText(DatabasePath, json);
        }
    }
}
