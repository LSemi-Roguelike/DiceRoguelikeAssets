using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public class Damage
    {
        public int damage;

        public Damage(int damage)
        {
            this.damage = damage;
        }
    }
}