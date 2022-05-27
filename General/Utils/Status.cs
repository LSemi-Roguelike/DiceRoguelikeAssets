namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Status
    {
        public int hp;
        public int armor;
        public int power;

        public Status(int hp, int armor, int power)
        {
            this.hp = hp;
            this.armor = armor;
            this.power = power;
        }

        public override string ToString()
        {
            return $"HP: {hp}, Power: {power}";
        }

        public static Status operator +(Status a, Status b)
        {
            return new Status(a.hp + b.hp, a.armor + b.armor, a.power + b.power);
        }

        public static Status operator -(Status a, Status b)
        {
            return new Status(a.hp - b.hp, a.armor - b.armor, a.power - b.power);
        }
    }
}