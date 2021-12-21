using System.Collections.Generic;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class State
    {
        public int hp;
        public List<Buff> buffs;
        public List<Debuff> debuffs;
    }
}