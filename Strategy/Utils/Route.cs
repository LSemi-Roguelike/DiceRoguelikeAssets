using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Route
    {
        public Vector3Int pos;
        public Route preRoute;
        public int dist;


        public Route(Vector3Int pos, Route preRoute, int dist)
        {
            this.pos = pos;
            this.preRoute = preRoute;
            this.dist = dist;
        }

        public static Route None => new Route(Vector3Int.zero, null, -1);

        public override string ToString()
        {
            return $"Pos:{pos}, Dist:{dist}, PreRoute:{preRoute?.pos}";
        }
    }
}