using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace LSemiRoguelike
{
    public class TileObstacle : TileContainer, IDamgeable
    {
        public IEnumerator GetDamage(Damage damage)
        {
            unit.GetDamage(damage);
            yield break;
        }
    }
}