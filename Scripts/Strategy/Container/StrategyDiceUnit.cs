using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyDiceUnit : StrategyUnit
    {
        public DiceUnit diceUnit => unit as DiceUnit;

        protected override void Init()
        {
            base.Init();

            diceUnit.SetDiceManager(SetActions);
        }

        private void SetActions(List<UnitAction> actions)
        {
            _actions = new List<StrategeAction>();
            for(int i = 0; i < actions.Count; i++)
            {
                _actions.Add(new StrategeAction(actions[i]));
                _actions[i].Instantiate(this);
            }
            nowAct = ActType.SelectAction;
        }

        private void SelectAction(int actNum)
        {
            _selectedAction = actNum;
            nowAct = ActType.SelectTarget;
        }

        protected override void TurnStart()
        {
            nowAct = ActType.WaitAction;
            diceUnit.GetAction();
        }

        protected override void TurnUpdate()
        {
            switch (nowAct)
            {
                case ActType.WaitAction:
                    break;
                case ActType.SelectAction:
                    if (_actions.Count == 0)
                    {
                        nowAct = ActType.TurnEnd;
                        break;
                    }
                    foreach (var action in _actions)
                        action.rangeObject.SetRange(action.range, this);

                    ActionSelectUI.inst.SetActionUI(_actions, SelectAction);
                    nowAct = ActType.WaitAction;
                    break;
                case ActType.SelectTarget:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3Int pos = InputManager.manager.GetMouseCellPos();
                        if (Acting(_actions[_selectedAction], pos))
                        {
                            Destroy(_actions[_selectedAction].rangeObject.gameObject);
                            _actions.RemoveAt(_selectedAction);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                        nowAct = ActType.SelectAction;
                    break;
                case ActType.TurnEnd:
                    _onTurn = false;
                    break;
            }
        }

        protected override void TurnEnd()
        {

        }

        public override void GetEffect(Effect effect)
        {

        }
    }
}