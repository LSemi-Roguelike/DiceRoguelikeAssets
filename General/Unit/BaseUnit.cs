using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class BaseUnit : MonoBehaviour, IHaveInfo
    {
        //info
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        public uint ID { get { return _id; } }
        public string Name { get { return _name; } }

        [SerializeField] protected Status maxStatus;
        [SerializeField] protected Ability ability;
        [SerializeField] protected Condition resist;
        protected Condition condition;
        protected Status status;

        public Status totalStatus { get; protected set; }

        protected BaseContainer _container;
        public BaseContainer container
        {
            get { return _container; }
            set { if (!_container) _container = value; }
        }

        public virtual void Init()
        {
            status = maxStatus;
            condition = new Condition();
        }

        public virtual bool IsDead => status.hp <= 0;

        public virtual void GetEffect(Effect effect)
        {
            status.armor += effect.status.armor;
            if (effect.status.hp < 0)
            {
                status.armor += effect.status.hp;
                if (status.armor < 0)
                {
                    status.hp += status.armor;
                    status.armor = 0;
                }
            }
            else if (effect.status.hp < 0)
            {
                status.hp += effect.status.hp;
                if (status.hp > maxStatus.hp)
                    status.hp = maxStatus.hp;
            }
            GetConditionEffect(effect.condition);
        }
        protected virtual void GetConditionEffect(Condition effect)
        {

        }
    }
}