using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategySkillUnit : StrategyActingUnit
    {
        public new SkillUnit Unit => base.Unit as SkillUnit;

        protected override void TurnStart()
        {

        }
        protected override void WaitAction()
        {

        }

        protected override void SelectAction()
        {
            if (_actions == null || _actions.Count == 0)
            {
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


    }
}