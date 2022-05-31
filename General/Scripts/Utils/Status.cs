namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Status
    {
        public float hp;
        public float shield;

        public Status(float hp, float shield)
        {
            this.hp = hp;
            this.shield = shield;
        }

        public override string ToString()
        {
            return $"HP: {hp}, Shield: {shield}";
        }

        public static Status operator +(Status a, Status b)
        {
            return new Status(a.hp + b.hp, a.shield + b.shield);
        }

        public static Status operator -(Status a, Status b)
        {
            return new Status(a.hp - b.hp, a.shield - b.shield);
        }
    }
}