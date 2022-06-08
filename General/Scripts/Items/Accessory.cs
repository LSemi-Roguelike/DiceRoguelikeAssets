using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Accessory : BaseItem<PassiveSkill>
    {
        public void Passive()
        {
            foreach (var skill in skills)
            {
                skill.Passive();
            }
        }
    }
}
