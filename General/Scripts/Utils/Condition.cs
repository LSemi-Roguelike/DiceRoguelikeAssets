using System.Collections.Generic;

namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Condition
    {
        [System.Serializable]
        public struct Info
        {
            public uint grade;
            public uint duration;
            public override string ToString()
            {
                return $"({grade}, {duration})";
            }
            public Info(uint grade, uint duration) { this.grade = grade; this.duration = duration; }
            public static Info operator +(Info a, Info b) => new Info(a.grade + b.grade, a.duration + b.duration);
            public static Info operator -(Info a, Info b) => new Info(a.grade - b.grade, a.duration - b.duration);
        }

        public Info burn;       //unstackable   damage
        public Info poison;     //stackable     damage
        public Info frozen;     //increase damage, decrease attack
        public Info shock;      //power down (if power < shock stun)
        
        public Info recovery;   //heal

        public bool wet;        //increase: frozen, shock   /   reduce: burn, poison
        public bool oiled;      //increase: burn, poison    /   reduce: frozen, shock

        public override string ToString()
        {
            string str = "";
            Info[] infos = { burn, poison, frozen, shock, recovery };
            string[] names = { "burn", "poison", "frozen", "shock", "recovery" };
            for(int i = 0; i < infos.Length; i++)
            {
                if (infos[i].duration > 0)
                    str += names[i] + infos[i]+ ",";
            }
            if (wet)
                str += "wet,";
            if (oiled)
                str += "oiled,";

            str = str.Substring(0, str.Length - 1);
            return str;
        }

        public static Condition operator +(Condition a, Condition b)
        {
            a.wet |= b.wet;
            a.oiled |= b.oiled;
            
            //burn
            if (b.burn.grade < 0)
            {
                a.burn.grade -= -b.burn.grade > a.burn.grade ? 0 : a.burn.grade - b.burn.grade;
            }
            else if (a.burn.grade < b.burn.grade)
            {
                a.burn = b.burn;
            }

            //a.poison.grade += b.poison.grade;
            //a.poison.grade

            return a;
        }
    }
}