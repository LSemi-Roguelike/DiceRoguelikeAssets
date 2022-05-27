using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Range
    {
        public enum RangeType { Linear, Distance, Square }

        public RangeType rangeType;
        public int minRange;
        public int maxRange;

        public bool ignoreBlocked;

        public Range(RangeType rangeType, int maxRange) : this(rangeType, 0, maxRange) { }

        public Range(RangeType rangeType, int minRange, int maxRange, bool ignoreBlocked = false)
        {
            this.rangeType = rangeType;
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.ignoreBlocked = ignoreBlocked;
        }

        public override string ToString()
        {
            return $"Type:{rangeType}, min:{minRange}, max:{maxRange}, blocked{ignoreBlocked}";
        }
    }
}