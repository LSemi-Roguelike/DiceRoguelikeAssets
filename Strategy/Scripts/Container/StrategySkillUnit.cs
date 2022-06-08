using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategySkillUnit : StrategyActingUnit
    {
        List<StrategyAction> _actions;
        public new SkillUnit Unit => base.Unit as SkillUnit;

        protected override void TurnStart()
        {
            Unit.Passive();
            Unit.GetAction();
        }
        protected override void WaitAction()
        {

        }

        protected override void SelectAction()
        {
            if (_actions == null || _actions.Count == 0)
            {
                Debug.Log("No Skill");
                nowAct = ActType.TurnEnd;
                return;
            }
            nowAct = ActType.TurnEnd;
        }

        protected override void SelectTarget()
        {
            nowAct = ActType.TurnEnd;
        }
        protected override void TurnEnd()
        {

        }

        protected override void SetActions(List<MainSkill> actions)
        {
            _actions = new List<StrategyAction>();
            for (int i = 0; i < actions.Count; i++)
            {
                _actions.Add(new StrategyAction(actions[i], cellPos));
            }
            nowAct = ActType.SelectAction;
        }

    }
}