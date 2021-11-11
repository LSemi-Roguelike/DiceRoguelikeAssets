using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 20)]
public class Weapon : BaseItem
{
    [SerializeField] SkillBase attackSkill;
    [SerializeField] SkillBase defenseSkill;
    [SerializeField] Status status;

    public Status GetStatus() { return status; }

    Unit owner;
    public void Init(Unit owner)
    {
        this.owner = owner;
    }

    public SkillBase Attack()
    {
        return attackSkill;
    }

    public SkillBase Defense()
    {
        return defenseSkill;
    }
}
