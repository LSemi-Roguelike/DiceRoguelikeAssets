using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class PassiveSkill : BaseSkill
    {
        public abstract IEnumerator Cast(BaseContainer owner);
    }
}