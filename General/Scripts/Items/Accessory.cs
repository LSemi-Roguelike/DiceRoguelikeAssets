using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Accessory : BaseItem
    {
        [SerializeField] protected PassiveSkill skillPrefab;
        protected PassiveSkill _skill;
        public PassiveSkill Skill { get { return _skill; } }

        public Accessory Init()
        {
            _skill = Instantiate(skillPrefab, transform);
            return this;
        }
    }
}
