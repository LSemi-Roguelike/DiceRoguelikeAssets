using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyAction
    {
        public MainSkill skill;
        public Route[] routes;
        public StrategyContainer[] targets;

        public StrategyAction(MainSkill skill, Vector3Int pos)
        {
            this.skill = skill;
            (routes, targets) = TileMapManager.manager.GetRangeTiles(pos, skill.range);
        }

        public void SetRange(Vector3Int pos)
        {
            (routes, targets) = TileMapManager.manager.GetRangeTiles(pos, skill.range);
        }

        public override string ToString()
        {
            return skill.ToString();
        }
    }
}