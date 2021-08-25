using HarmonyLib;
using Jotunn;
using ElliteClans.Server;
using System;
using Log = Jotunn.Logger;

namespace ElliteClans.Patches
{
    [HarmonyPatch]
    public class GamePatches
    {
        [HarmonyPatch(typeof(Game), "Start")]
        [HarmonyPrefix]
        public static void GameStart()
        {
            Startup.IsServer = ZNet.instance.IsServer() || ZNet.instance.IsDedicated() || ZNet.instance.IsLocalInstance();
            Startup.IsClient = ZNet.instance.IsClientInstance();

            if (Startup.IsServer)
            {
                Log.LogInfo("Server instance found");

                ServerManager.LoadClans();

                RegisterServerRPCs();
            }

            if (Startup.IsClient)
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
