using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 20)]
public class Weapon : Item
{
    [SerializeField]
    SkillBase attackAct = null;

    private void OnEnable()
    {
        itemType = ItemType.WEAPONE;
    }

    public SkillBase GetAttackAct()
    {
        attackAct.SetCaster(owner);
        return attackAct;
    }
}
