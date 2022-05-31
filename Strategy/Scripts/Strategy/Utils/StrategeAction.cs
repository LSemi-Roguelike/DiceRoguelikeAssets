using System;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategeAction : UnitAction
    {
        public RangeObject rangeObject;

        public StrategeAction(UnitAction unitAction) : base(unitAction) { }

        public StrategeAction(MainSkill skill, Range range) : base(skill, range) { }
        public StrategeAction(Range movement) : base(movement) { }

        public void Instantiate(StrategyContainer actor)
        {
            base.Instantiate(actor.transform);
            rangeObject = GameObject.Instantiate(TileMapManager.manager.rangeViewerPrefab, actor.transform);
        }
    }
}
