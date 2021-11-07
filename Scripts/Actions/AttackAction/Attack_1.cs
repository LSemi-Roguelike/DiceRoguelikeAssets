using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_1 : SkillBase, IAttackAction
{
    public void AttackAction(int dist)
    {
        throw new System.NotImplementedException();
    }

    public override void Cast(Character target)
    {
        base.Cast(target);
        Debug.Log(caster.GetName() + " attack to " + target.GetName());
    }
}
