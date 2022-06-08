using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Move : MainSkill
    {
        public override IEnumerator Cast(BaseContainer target) { yield break; }
        public override string ToString()
        {
            return "Move";
        }
    }
}