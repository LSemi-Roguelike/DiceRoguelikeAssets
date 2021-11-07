using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Range
{
    public enum RangeType { AXIS_CROSS, AXIS_DIAGONAL, AXIS_ALL, DISTANCE, SQUARE}

    public RangeType rangeType;
    public bool ignoreBlocked;

    public int minRange;
    public int maxRange;
}
