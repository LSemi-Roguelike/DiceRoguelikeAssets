using System;
using System.Collections;
using UnityEngine;

namespace LSemiRoguelike
{
    public interface IDamageable
    {
        public IEnumerator GetDamage(Damage damage);
    }
}