using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceUnit : TileUnit
{
    [SerializeField] DiceManager diceManager;

    protected override IEnumerator TurnStart()
    {
        yield return null;
    }

    protected override IEnumerator TurnEnd()
    {
        yield return null;
    }

    protected override IEnumerator BehaviorSelect(Action<UnitBehavior> action)
    {
        yield return Utils.waitAnyKey;

        if (Input.GetKeyDown(KeyCode.Q))
            action(UnitBehavior.MOVE);
        else if (Input.GetKeyDown(KeyCode.W))
            action(UnitBehavior.ATTACK);
        else if (Input.GetKeyDown(KeyCode.Escape))
            action(UnitBehavior.END);
        else
            action(UnitBehavior.IDLE);
    }

    protected override IEnumerator SelectMovement(System.Action<Movement> action)
    {
        Movement movement = null;
        yield return StartCoroutine(diceManager.SelectMoveDice((a) => { movement = a; }));

        action(movement);
    }

    protected override IEnumerator SelectSkill(Action<Skill> action)
    {
        yield return Utils.waitAnyKey;
        Skill skill = null;
        yield return StartCoroutine(diceManager.SelectSkillDice((a) => { skill = a; }));

        action(skill);
    }

    protected override IEnumerator MovePointSelect(Action<int> action)
    {
        yield return Utils.waitAnyKey;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int pos = InputManager.manager.GetMouseCellPos();
            for (int i = 0; i < rangeRoutes.Length; i++)
            {
                if (rangeRoutes[i].pos == pos)
                {
                    action(i);
                    yield break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            action(-2);
        else
            action(-1);
    }

    protected override IEnumerator AttackTargetSelect(Action<int> action)
    {
        yield return Utils.waitAnyKey;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int pos = InputManager.manager.GetMouseCellPos();
            for (int i = 0; i < rangeRoutes.Length; i++)
            {
                if (rangeRoutes[i].pos == pos)
                {
                    action(i);
                    yield break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            action(-2);
        else
            action(-1);
    }

    public override void GetAttack(Damage damage)
    {

    }
}
