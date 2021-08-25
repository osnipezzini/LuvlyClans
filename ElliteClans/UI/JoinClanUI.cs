
using UnityEngine;

namespace ElliteClans.UI
{
    class JoinClanUI : MonoBehaviour
    {
        public static JoinClanUI Instance;
        private Rect windowRect, lastRect, errorWinRect;
        private Rect dragRect = new Rect(0, 0, 10000, 20);

        void OnGUI()
        {
            windowRect = GUILayout.Window(1586463, windowRect, DrawConnectWindow, "Clans");
            if (!lastRect.Equals(windowRect))
            {
                Startup.windowPosX.Value = windowRect.x;
                Startup.windowPosY.Value = windowRect.y;
                lastRect = windowRect;
            }
        }

        private void DrawConnectWindow(int id)
        {
            GUI.DragWindow(dragRect);
            GUILayout.Button("Entrar em clan existente");
        }

        void Awake()
        {
            windowRect.x = Startup.windowPosX.Value;
            windowRect.y = Startup.windowPosY.Value;
            windowRect.width = 250;
            windowRect.height = 50;
            lastRect = windowRect;
        }
    }
}
