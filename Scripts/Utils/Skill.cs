using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Skill
{
    public int cost;
    public Range range;
    public SkillBase skill;

    public Skill(int cost, Range range, SkillBase skill)
    {
        this.cost = cost;
        this.range = range;
        this.skill = skill;
    }
}