using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LSemiRoguelike
{
    public class Weapon : BaseItem
    {
        [SerializeField] private Status status;
        [SerializeField] private UnitAction _unitAction;
        private ItemUnit _unit;
        public UnitAction unitAction => _unitAction;
        public Status GetStatus() { return status; }

        public Weapon Init(ItemUnit unit)
        {
            _unit = unit;
            return this;
        }
    }
}