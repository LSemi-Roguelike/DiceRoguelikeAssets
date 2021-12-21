using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class BaseContainer : MonoBehaviour, IHaveInfo
    {
        [SerializeField] protected string _id;
        [SerializeField] protected string _name;

        private BaseUnit baseUnit;
        protected BaseUnit _unit;
        public BaseUnit unit { get { return _unit; } }

        public string ID { get { return _id; } }

        public string Name { get { return _name; } }

        public void Init(BaseUnit baseUnit)
        {
            this.baseUnit = baseUnit;
            Init();
        }

        protected virtual void Init()
        {
            _unit = Instantiate(baseUnit, transform);
            _unit.container = this;
        }
    }
}