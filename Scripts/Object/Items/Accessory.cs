using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Accessory", menuName = "Items/Accessory", order = 0)]
    public abstract class Accessory : BaseItem
    {
        [SerializeField] protected PassiveSkill _skill;
        public PassiveSkill Skill { get { return _skill; } }
    }
}
