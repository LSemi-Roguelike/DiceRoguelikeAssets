using System.Collections.Generic;

namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Condition
    {
        [System.Serializable]
        public struct Info
        {
            public int duration;
            public int grade;
        }

        public Info burn;
        public Info frozen;
        public Info discharge;
        public Info shock;
        public Info poison;
        public Info recovery;
        public Info wet;
    }
}