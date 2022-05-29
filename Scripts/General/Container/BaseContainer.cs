using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseContainer : MonoBehaviour, IHaveInfo
    {
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        [SerializeField] protected BaseUnit _unit;
        [SerializeField] protected UnitStatusUI _statusUI;

        public BaseUnit unit { get { return _unit; } }

        public uint ID { get { return _id; } }

        public string Name { get { return _name; } }

        public virtual Vector3 Pos => transform.position;

        public virtual void GetEffect(Effect effect)
        {
            _unit.GetEffect(effect);
            _statusUI.SetUI(_unit.status);
        }

        public void Init(BaseUnit baseUnit)
        {
            _unit = baseUnit;
            _unit.Init();
            Init();
        }

        protected virtual void Init()
        {
            _unit.container = this;
            _statusUI.InitUI(_unit.maxStatus);
            _statusUI.SetUI(_unit.status);
        }

        protected virtual void SetStatusUI()
        {
            _statusUI.SetUI(_unit.status);
        }
    }
}