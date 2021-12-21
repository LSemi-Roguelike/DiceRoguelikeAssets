using System;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class ItemUnit : ActorUnit
    {
        [SerializeField]
        protected Weapon weapon;
        [SerializeField]
        protected Parts armParts, legParts, bodyParts;
        [SerializeField]
        protected List<Accessory> accessory;

        public override void Init()
        {
            base.Init();
        }

        public BaseSkill GetMainSkill()
        {
            return weapon?.Skill;
        }

        public List<SubSkill> GetSubSkills(Parts.PartsType pType)
        {
            switch (pType)
            {
                case Parts.PartsType.ARM:
                    return armParts?.GetSkills();
                case Parts.PartsType.LEG:
                    return legParts?.GetSkills();
                case Parts.PartsType.BODY:
                    return bodyParts?.GetSkills();
                default:
                    return null;
            }
        }

        public List<PassiveSkill> GetPassiveSkills()
        {
            List<PassiveSkill> list = new List<PassiveSkill>();
            foreach (var temp in accessory)
            {
                list.Add(temp.Skill);
            }
            return list;
        }

        protected override void SetTotalStatus()
        {
            totalStatus = new Status();

            totalStatus += unitStatus;

            totalStatus += armParts.GetStatus();
            totalStatus += legParts.GetStatus();
            totalStatus += bodyParts.GetStatus();
            totalStatus += weapon.GetStatus();

            totalStatus *= statusMagnify;
        }
    }
}