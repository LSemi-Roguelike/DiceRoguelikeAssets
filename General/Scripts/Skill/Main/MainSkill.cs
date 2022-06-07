using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class MainSkill : BaseSkill
    {
        public Range range;
        public int targetLayer;
        public abstract IEnumerator Cast(BaseContainer target);
    }
}