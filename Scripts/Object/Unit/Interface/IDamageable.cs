using System;
using System.Collections;
using UnityEngine;

namespace LSemiRoguelike
{
    public interface IDamgeable
    {
        public IEnumerator GetDamage(Damage damage);
    }
}