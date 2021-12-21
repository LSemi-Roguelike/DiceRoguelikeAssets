using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 10)]
    public class Weapon : BaseItem
    {
        [SerializeField] MainSkill _skill;
        [SerializeField] Status status;
        public BaseSkill Skill { get { return _skill; } }
        public Status GetStatus() { return status; }
    }
}