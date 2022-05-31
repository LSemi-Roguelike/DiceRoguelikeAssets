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
        }

        public Info burn;       //unstackable   damage
        public Info poison;     //stackable     damage
        public Info frozen;     //
        public Info shock;      //power down (if power < shock stun)
        
        public Info recovery;   //heal

        public uint wet;        //increase: frozen, shock   /   reduce: burn, poison
        public uint oiled;      //increase: burn, poison    /   reduce: frozen, shock

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
            if (wet != 0)
                str += "wet: " + wet + ",";
            if (oiled != 0)
                str += "oiled: " + oiled + ",";

            str = str.Substring(0, str.Length - 1);
            return str;
        }
    }
}