using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class UnitAction
    {
        public Range range;
        public MainSkill skill;
        public Sprite sprite;
        public UnitAction(MainSkill skill, Range range)
        {
            this.range = range;
            this.skill = skill;
            sprite = null;
        }
        public UnitAction(Range movement)
        {
            range = movement;
            skill = MainSkill.Movement;
            sprite = null;
        }
        public UnitAction(UnitAction other)
        {
            this.range = other.range;
            this.skill = other.skill;
            this.sprite = other.sprite;
        }

        public void Instantiate(Transform actor)
        {
            if(skill)
                skill = GameObject.Instantiate(skill, actor);
        }

        public override string ToString()
        {
            return $"Range: {range}\nSkill: {(skill == null ? "Move" : skill.ToString())}";
        }
    }
}