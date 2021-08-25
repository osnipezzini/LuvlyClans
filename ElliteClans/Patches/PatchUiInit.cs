using ElliteClans.UI;

using HarmonyLib;

using UnityEngine;

namespace ElliteClans.Patches
{
    [HarmonyPatch(typeof(FejdStartup), "SetupGui")]
    class PatchUiInit
    {
        static void Postfix()
        {
            if (!JoinClanUI.Instance)
            {
                var go = new GameObject("ElliteClans");
                JoinClanUI.Instance = go.AddComponent<JoinClanUI>();
            }
        }
    }
}
