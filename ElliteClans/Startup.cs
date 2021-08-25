// LuvlyClans
// a Valheim mod skeleton using Jötunn
// 
// File:    LuvlyClans.cs
// Project: LuvlyClans

using BepInEx;
using BepInEx.Configuration;

using ElliteClans.Server;

using HarmonyLib;

using Jotunn;
using Jotunn.Managers;
using Jotunn.Utils;

using ElliteClans.Patches;
using ElliteClans.Server.Types;

using JSON = SimpleJson.SimpleJson;
using Log = Jotunn.Logger;

namespace ElliteClans
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class Startup : BaseUnityPlugin
    {
        public const string PluginGUID = "com.ellitedev.elliteclans";
        public const string PluginName = "ElliteClans";
        public const string PluginVersion = "1.0.0";

        public static string ConfigPath => BepInEx.Paths.ConfigPath;
        public const string DatabasePath = "luvly.clans.json";
        public const string BackupDatabasePath = "luvly.clans.old.json";
        private ConfigEntry<string> entryConfigPath;

        public static bool IsServer;
        public static bool IsClient;
        public static ConfigEntry<float> windowPosX;
        public static ConfigEntry<float> windowPosY;
        public static DB DatabaseServer;
        public static Clans ClansServer;
        public static Clans ClansClient;
        public static Clans ClansDatabase;

        private readonly Harmony harmony = new Harmony(PluginGUID);

        private void Awake()
        {
            CreateConfigValues();

            Log.LogInfo("Loading patches");

            harmony.PatchAll(typeof(GamePatches));
            harmony.PatchAll(typeof(ZNetPatches));
            harmony.PatchAll(typeof(DoorPatches));
            harmony.PatchAll(typeof(ContainerPatches));
            harmony.PatchAll(typeof(MinimapPatches));
            harmony.PatchAll(typeof(ShipControllsPatches));
            harmony.PatchAll(typeof(TeleportWorldPatches));
            harmony.PatchAll(typeof(CharacterPatches));
            harmony.PatchAll(typeof(EnemyHudPatches));
            harmony.PatchAll(typeof(PatchUiInit));
        }

        private void Update()
        {
            return;
        }

        public void CreateConfigValues()
        {
            Config.SaveOnConfigSet = true;
            
            windowPosX = Config.Bind("UI", "WindowPosX", 20f);
            windowPosY = Config.Bind("UI", "WindowPosY", 20f);
            
            entryConfigPath = Config.Bind(
                "Server Config",
                "DB_PATH",
                ConfigPath + DatabasePath,
                new ConfigDescription("absolute path to clans db", null, new ConfigurationManagerAttributes { IsAdminOnly = true }));

            SynchronizationManager.OnConfigurationSynchronized += (obj, attr) =>
            {
                if (attr.InitialSynchronization)
                {
                    Log.LogMessage("Initial Config sync event received");
                }
                else
                {
                    Log.LogMessage("Config sync event received");
                }
            };
        }

        public static string GetServerClansJSON()
        {
            return JSON.SerializeObject(ClansServer);
        }
    }
}