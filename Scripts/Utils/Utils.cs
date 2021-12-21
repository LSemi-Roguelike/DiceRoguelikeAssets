using UnityEngine;

namespace LSemiRoguelike
{
    public static class Utils
    {
        public static readonly int StdTurnPoint = 3;
        public static int GetTurnCount(int speed)
        {
            return (int)(100 / (speed + 25));
        }

        public static readonly float tileMoveSpeed = 5;
        public static readonly float cameraMoveTime = 0.1f;

        public static WaitUntil waitAnyKey = new WaitUntil(() => { return Input.anyKeyDown; });
    }
}