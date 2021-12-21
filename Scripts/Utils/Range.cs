using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public class Range
    {
        public enum RangeType { AXIS_CROSS, AXIS_DIAGONAL, AXIS_ALL, DISTANCE, SQUARE }

        public RangeType rangeType;
        public int minRange;
        public int maxRange;

        public bool ignoreBlocked;

        public Range(RangeType rangeType, int minRange, int maxRange, bool ignoreBloked = false)
        {
            this.rangeType = rangeType;
            this.ignoreBlocked = ignoreBloked;
            this.minRange = minRange;
            this.maxRange = maxRange;
        }
    }
}