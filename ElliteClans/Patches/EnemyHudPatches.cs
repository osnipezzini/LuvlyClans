﻿using HarmonyLib;
using UnityEngine;
using Log = Jotunn.Logger;

namespace ElliteClans.Patches
{
    [HarmonyPatch]
    public class EnemyHudPatches
    {
        [HarmonyPatch(typeof(EnemyHud), "TestShow")]
        [HarmonyPrefix]
        public static bool EnemyHudTestShow(EnemyHud __instance, Character c)
        {
			float num = Vector3.SqrMagnitude(c.transform.position - __instance.m_refPoint);

			if (c.IsPlayer())
			{
				if (num < __instance.m_maxShowDistance * __instance.m_maxShowDistance)
				{
					if (c.IsCrouching())
					{
						return false;
					}

					Player player = Player.m_localPlayer;
					Player enemy = c as Player;

					bool canSee = ClansHelper.IsSameClanByPlayerName(player.GetPlayerName(), enemy.GetPlayerName(), true);

					if (!canSee)
					{
						return false;
					}
				}
			}

			return true;
		}
    }
}