using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Movement
{
    public int cost;
    public int movePoint;
    public bool diagonalMove;

    public Movement(int cost, int movePoint, bool diagnalMove = false)
    {
        this.cost = cost;
        this.movePoint = movePoint;
        this.diagonalMove = diagnalMove;
    }
}