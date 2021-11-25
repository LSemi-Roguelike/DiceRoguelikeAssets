using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : BaseUnit
{

    [SerializeField] protected Status charStatus;
    [SerializeField] protected Status statusMagnify = Status.one;

    protected Status buffStatus;
    protected Status totalStatus;

    [SerializeField]
    protected Weapon weapon;
    [SerializeField]
    protected Parts armParts, legParts, bodyParts;
    protected List<SkillBase> Skills;

    public virtual void Init()
    {
        SetTotalStatus();
    }

    public List<SkillBase> AttackAct()
    {
        return armParts?.GetSkills();
    }
    public List<SkillBase> MoveAct()
    {
        return legParts?.GetSkills();
    }

    public List<SkillBase> GetDamage()
    {
        return bodyParts?.GetSkills();
    }

    public int GetTurnPoint()
    {
        return 3;
    }

    public SkillBase GetAttackSkill()
    {
        return weapon.Attack();
    }

    protected virtual void SetTotalStatus()
    {
        totalStatus = new Status();

        totalStatus += charStatus;

        totalStatus += armParts.GetStatus();
        totalStatus += legParts.GetStatus();
        totalStatus += bodyParts.GetStatus();
        totalStatus += weapon.GetStatus();
    }

    public Status GetStatus()
    {
        return totalStatus;
    }
}