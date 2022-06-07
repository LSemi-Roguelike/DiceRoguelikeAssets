using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyDiceUnit : StrategyUnit
    {
        public PlayerUnit diceUnit => unit as PlayerUnit;

        protected int _selectedAction;
        protected List<StrategyAction> _actions;

        public override void Init()
        {
            base.Init();

            diceUnit.SetActionCallback(SetActions);
        }

        private void SetActions(List<MainSkill> actions)
        {
            _actions = new List<StrategyAction>();
            for(int i = 0; i < actions.Count; i++)
            {
                _actions.Add(new StrategyAction(actions[i], cellPos));
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
                        action.SetRange(cellPos);

                    ActionSelectUI.inst.SetActionUI(_actions, SelectAction);
                    nowAct = ActType.WaitAction;
                    break;
                case ActType.SelectTarget:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3Int pos = InputManager.manager.GetMouseCellPos();
                        if (Acting(_actions[_selectedAction], pos))
                        {
                            _actions.RemoveAt(_selectedAction);
                            RangeViewManager.Inst.HideRange();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        RangeViewManager.Inst.HideRange();
                        nowAct = ActType.SelectAction;
                    }
                    break;
                case ActType.TurnEnd:
                    _onTurn = false;
                    break;
            }
        }

        protected override void TurnEnd()
        {

        }
    }
}