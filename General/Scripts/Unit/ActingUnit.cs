using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class ActingUnit : BaseUnit
    {
        public abstract void SetActionCallback(System.Action<List<MainSkill>> action);
        public abstract void GetAction();
        public abstract void Attack(float damage);
        public abstract void Move(float movement);
        public abstract void Damaged(float damage);
        public override void GetEffect(Effect effect)
        {
            base.GetEffect(effect);
            Damaged(effect.status.hp);
        }
    }
}
