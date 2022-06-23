using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class ActingUnit : BaseUnit
    {
        public abstract void SetActionCallback(System.Action<List<MainSkill>> action);
        public abstract void GetSkill();
        public abstract void Attack();
        public abstract void Move();
        protected abstract void Damaged();
        public abstract void Passive();
        public override void GetEffect(Effect effect)
        {
            base.GetEffect(effect);
            Damaged();
        }
    }
}
