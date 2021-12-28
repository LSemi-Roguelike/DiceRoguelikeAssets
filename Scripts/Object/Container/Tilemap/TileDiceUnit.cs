using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class TileDiceUnit : TileUnit, IDamageable
    {
        [SerializeField] DiceManager _diceManager;
        DiceManager diceManager;
        public DiceUnit diceUnit { get { return unit as DiceUnit; } }

        protected override void Init()
        {
            base.Init();

            diceManager = Instantiate(_diceManager);
            diceManager.transform.SetParent(transform, true);
        }

        protected override IEnumerator TurnStart()
        {
            Debug.Log(diceManager.name + " turn start");
            var skills = diceUnit.GetPassiveSkills();
            foreach (var skill in skills)
            {
                yield return StartCoroutine(SkillCasting(skill));
            }
        }

        protected override IEnumerator TurnEnd()
        {
            Debug.Log(diceManager.name + " turn end");
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

        protected override IEnumerator SelectMovement(Action<int> action)
        {
            int movement = -1;
            yield return StartCoroutine(_diceManager.SelectMoveDice((a) => { movement = a; }));

            action(movement);
        }

        protected override IEnumerator SelectSkill(Action<MainSkill> action)
        {
            MainSkill skill = null;
            yield return StartCoroutine(_diceManager.SelectSkillDice((a) => { skill = a; }));

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

        protected override IEnumerator MoveEnd(int movement)
        {
            var skills = diceUnit.GetSubSkills(Parts.PartsType.LEG);
            foreach (var skill in skills)
            {
                yield return StartCoroutine(SkillCasting(skill));
            }
        }

        protected override IEnumerator AttackEnd(TileContainer target)
        {
            var skills = diceUnit.GetSubSkills(Parts.PartsType.ARM);
            foreach (var skill in skills)
            {
                yield return StartCoroutine(SkillCasting(skill, target));
            }
        }

        protected override IEnumerator GetDamageEnd(Damage damage)
        {
            var skills = diceUnit.GetSubSkills(Parts.PartsType.BODY);
            foreach (var skill in skills)
            {
                yield return StartCoroutine(SkillCasting(skill));
            }
        }
    }
}