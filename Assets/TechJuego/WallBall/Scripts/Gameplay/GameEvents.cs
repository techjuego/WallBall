using System;

namespace TechJuego.Framework
{
    public class GameEvents
    {
        public delegate void OnAction();
        public static OnAction LevelFailed;
        public static OnAction OnShowRateUs;
        public static OnAction OnStarCollect;
        public static OnAction ShakeCameraBig;
        public static OnAction ShakeCameraSmall;
    }
}