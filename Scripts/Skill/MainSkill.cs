using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class MainSkill : BaseSkill
    {
        [SerializeField] protected Range _range;        
        public Range range { get { return _range; } }
    }
}