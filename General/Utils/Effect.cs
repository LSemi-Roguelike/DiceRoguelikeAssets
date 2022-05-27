using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Effect
    {
        public enum EffectType { Main, Sub, Passive, Other }

        public EffectType effectType;

        //hp : heal / damage
        //armor : charge/ damage
        //power : power charge / discharge
        public Status status;

        //conditions
        public Condition condition;

        // buff / debuff
        public Ability ability;

        public Vector3 knockback;

        public Effect(EffectType effectType, Status status, Condition condition, Ability ability, Vector3 knockback)
        {
            this.effectType = effectType;
            this.status = status;
            this.condition = condition;
            this.ability = ability;
            this.knockback = knockback;
        }
    }
}