using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public interface IAttackable
{
    public void GetAttack(Damage damage);
}
