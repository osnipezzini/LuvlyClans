using HarmonyLib;
using Log = Jotunn.Logger;

namespace ElliteClans.Patches
{
    [HarmonyPatch]
    public class ShipControllsPatches
    {
        [HarmonyPatch(typeof(ShipControlls), "Interact")]
        [HarmonyPrefix]
        public static bool ShipControllsInteract(ShipControlls __instance, Humanoid character, bool repeat)
        {
            Player player = character as Player;
            Ship ship = __instance.GetShip();

            if (player && ship)
            {
                Piece shipPiece = ship.GetComponentInParent<Piece>();

                if (shipPiece)
                {
                    if (player.GetPlayerID() == shipPiece.GetCreator())
                    {
                        bool canInteract = ClansHelper.IsSameClanByPlayerID(shipPiece.GetCreator(), player.GetPlayerID(), true);

                        if (!canInteract)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
