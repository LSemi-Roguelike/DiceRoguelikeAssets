using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace LSemiRoguelike.Strategy
{
    public class StrategyObstacle : StrategyContainer
    {
        protected override void Init()
        {
            base.Init();
            _statusUI.gameObject.SetActive(false);
        }
        public override void GetEffect(Effect effect)
        {
            base.GetEffect(effect);
            _statusUI.gameObject.SetActive(true);
        }
    }
}