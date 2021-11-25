using UnityEngine;

public static class Utils
{
    public static int GetTurnCount(int speed)
    {
        return (int)(100 / speed + 25);
    }

    public static readonly float tileMoveSpeed = 5;

    public static WaitUntil waitAnyKey = new WaitUntil(() => { return Input.anyKeyDown; });
}