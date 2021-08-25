using HarmonyLib;
using Jotunn;
using LuvlyClans.Server;
using System;
using Log = Jotunn.Logger;

namespace LuvlyClans.Patches
{
    [HarmonyPatch]
    public class GamePatches
    {
        [HarmonyPatch(typeof(Game), "Start")]
        [HarmonyPrefix]
        public static void GameStart()
        {
            Startup.m_is_server = ZNet.instance.IsServer() || ZNet.instance.IsDedicated() || ZNet.instance.IsLocalInstance();
            Startup.m_is_client = ZNet.instance.IsClientInstance();

            if (Startup.m_is_server)
            {
                Log.LogInfo("Server instance found");

                ServerManager.LoadClans();

                RegisterServerRPCs();
            }

            if (Startup.m_is_client)
            {
                Log.LogInfo("Client instance found");

                RegisterClientRPCs();
            }
        }

        public static void RegisterServerRPCs()
        {
            Log.LogInfo("Registering Clans RPCs");
            ZRoutedRpc.instance.Register("RequestClans", new Action<long, ZPackage>(Server.RPC.RPC_RequestClans));
            ZRoutedRpc.instance.Register("ResponseClans", new Action<long, ZPackage>(Server.RPC.RPC_ResponseClans));
            ZRoutedRpc.instance.Register("BadMessage", new Action<long,ZPackage>(Server.RPC.RPC_BadRequest));
        }

        public static void RegisterClientRPCs()
        {
            Log.LogInfo("Registering Clans RPCs");
            ZRoutedRpc.instance.Register("RequestClans", new Action<long, ZPackage>(Client.RPC.RPC_RequestClans));
            ZRoutedRpc.instance.Register("ResponseClans", new Action<long, ZPackage>(Client.RPC.RPC_ResponseClans));
            ZRoutedRpc.instance.Register("BadMessage", new Action<long, ZPackage>(Client.RPC.RPC_BadRequest));
        }
    }
}
