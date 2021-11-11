using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    string id;
    [SerializeField]
    string charName;

    [SerializeField] protected Status charStatus;
    [SerializeField] protected Status statusMagnify;

    protected Status buffStatus;
    protected Status totalStatus;

    [SerializeField]
    protected Weapon weapon;
    [SerializeField]
    protected Parts armParts, legParts, bodyParts;
    protected List<SkillBase> Skills;

    public string GetName() { return charName; }
    public string GetID() { return id; }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
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

    public int GetMovePoint()
    {
        return (int)totalStatus.speed;
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

        Debug.Log(totalStatus.ToString());
    }
}