using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyDiceUnit : StrategyActingUnit
    {
        public new PlayerUnit Unit => base.Unit as PlayerUnit;

        protected int _selectedAction;
        protected List<StrategyAction> _actions;

        public override void Init()
        {
            base.Init();

        }

        protected override void SetActions(List<MainSkill> actions)
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
            Unit.GetAction();
        }

        protected override void WaitAction()
        {

        }

        protected override void SelectAction()
        {
            if (_actions.Count == 0)
            {
                nowAct = ActType.TurnEnd;
                return;
            }
            foreach (var action in _actions)
                action.SetRange(cellPos);

            ActionSelectUI.Inst.SetActionUI(_actions, SelectAction);
            nowAct = ActType.WaitAction;
        }

        protected override void SelectTarget()
        {
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
        }

        protected override void TurnEnd()
        {

        }

    }
}