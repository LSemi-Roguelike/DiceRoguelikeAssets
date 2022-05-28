using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class SubSkill : BaseSkill
    {
        public abstract IEnumerator Cast(BaseContainer owner, float power);
    }
}