using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class MainSkill : BaseSkill
    {
        public static MainSkill Movement => null;
        public static int MoveCost(Range range)
        {
            return range.maxRange;
        }
        public int targetLayer;
        public abstract IEnumerator Cast(BaseUnit caster, BaseContainer target);
    }
}