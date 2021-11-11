using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}