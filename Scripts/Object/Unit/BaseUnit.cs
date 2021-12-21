using System;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class BaseUnit : MonoBehaviour, IHaveInfo
    {
        //info
        [SerializeField] protected string _id;
        [SerializeField] protected string _name;
        public string ID { get { return _id; } }
        public string Name { get { return _name; } }

        [SerializeField] protected Status unitStatus;
        protected State state;

        public Status totalStatus { get; protected set; }

        protected BaseContainer _container;
        public BaseContainer container
        {
            get { return _container; }
            set { if (!_container) _container = value; }
        }

        public virtual void Init()
        {
            SetTotalStatus();
            state = new State();
            state.hp = totalStatus.maxHP;
        }

        protected virtual void SetTotalStatus()
        {
            totalStatus = unitStatus;
        }

        public virtual bool IsDead()
        {
            return state.hp <= 0;
        }

        public virtual bool GetDamage(Damage damage)
        {
            state.hp -= damage.damage;
            return IsDead();
        }
    }
}