using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiceManager : DiceManager
{
    //for test
    [SerializeField] MoveDiceParts moveDiceParts;
    [SerializeField] SkillDiceParts skillDiceParts;
    public override IEnumerator SelectMoveDice(Action<Movement> action)
    {
        //select dice
        yield return Utils.waitAnyKey;
        if (Input.GetKeyDown(KeyCode.Escape))
            action(null);
        else
            action(moveDiceParts.getMovement);
    }

    public override IEnumerator SelectSkillDice(Action<Skill> action)
    {
        //select dice
        yield return Utils.waitAnyKey;
        if (Input.GetKeyDown(KeyCode.Escape))
            action(null);
        else
            action(skillDiceParts.getDiceSkill);
    }
}
