using System;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BaseUnit, IDamgeable
{
    [SerializeField]
    int hp;
    public void GetDamage(Damage damage)
    {
        hp -= damage.damage;
    }
}