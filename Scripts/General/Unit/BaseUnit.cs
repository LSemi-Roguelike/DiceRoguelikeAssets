using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class BaseUnit : MonoBehaviour, IHaveInfo
    {
        [Header("Info")]
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        public uint ID { get { return _id; } }
        public string Name { get { return _name; } }

        [Header("Status")]
        [SerializeField] protected Status _maxStatus;
        [SerializeField] protected Ability _ability;
        [SerializeField] protected Condition _resist;
        protected Condition _condition;
        protected Status _status;

        public Status status => _status;
        public Status maxStatus => _maxStatus;

        protected BaseContainer _container;
        public BaseContainer container
        {
            get { return _container; }
            set { if (!_container) _container = value; }
        }

        public virtual void Init()
        {
            _status = _maxStatus;
            _condition = new Condition();
        }

        public virtual bool IsDead => _status.hp <= 0;

        public virtual Effect SetEffect(Effect effect)
        {
            effect.status.hp += effect.status.hp*_ability.attackMulti;
            if(effect.status.hp > 0)
                effect.status.hp += _ability.attackIncrese;
            else if(effect.status.hp < 0)
                effect.status.hp -= _ability.attackIncrese;

            return effect;
        }

        public virtual void GetEffect(Effect effect)
        {
            _status.shield += effect.status.shield;
            if (effect.status.hp < 0)
            {
                _status.shield += effect.status.hp;
                if (_status.shield < 0)
                {
                    _status.hp += _status.shield;
                    _status.shield = 0;
                }
            }
            else if (effect.status.hp > 0)
            {
                _status.hp += effect.status.hp;
                if (_status.hp > _maxStatus.hp)
                    _status.hp = _maxStatus.hp;
            }
            GetConditionEffect(effect.condition);
        }
        protected virtual void GetConditionEffect(Condition effect)
        {

        }
    }
}