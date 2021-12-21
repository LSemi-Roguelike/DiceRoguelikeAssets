using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class PlayerDiceManager : DiceManager
    {
        //for test
        [SerializeField] int movement;
        [SerializeField] MainSkill skill;
        public override IEnumerator SelectMoveDice(Action<int> action)
        {
            //select dice
            yield return Utils.waitAnyKey;
            if (Input.GetKeyDown(KeyCode.Escape))
                action(-1);
            else
                action(movement);
        }

        public override IEnumerator SelectSkillDice(Action<MainSkill> action)
        {
            //select dice
            yield return Utils.waitAnyKey;
            if (Input.GetKeyDown(KeyCode.Escape))
                action(null);
            else
                action(skill);
        }
    }
}

