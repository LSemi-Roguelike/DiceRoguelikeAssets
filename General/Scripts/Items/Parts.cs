using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Parts : BaseItem<SubSkill>
    {
        public enum PartsType { ARM, LEG, BODY }

        [SerializeField] protected PartsType type;
        [SerializeField] protected float powerGen = 1f;

        public PartsType GetPartsType() { return type; }

        public void PowerGenerate()
        {
            foreach (var skill in skills)
            {
                skill.SupplyPower(powerGen);
            }
        }
    }
}