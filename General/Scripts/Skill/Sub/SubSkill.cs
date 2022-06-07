using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class SubSkill : BaseSkill
    {
        [SerializeField] private float _cost;
        private float _nowPower;

        public override void Init(ActingUnit caster)
        {
            _nowPower = 0;
            base.Init(caster);
        }

        public void SupplyPower(float power)
        {
            _nowPower += power;
            if (_nowPower > _cost)
            {
                StartCoroutine(Cast());
                _nowPower -= _cost;
            }
        }

        protected abstract IEnumerator Cast();
    }
}