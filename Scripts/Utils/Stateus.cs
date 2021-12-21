namespace LSemiRoguelike
{
    [System.Serializable]
    public class Status
    {
        public int maxHP;
        public int attack;
        public int defense;
        public int secure;
        public int speed;
        public int hacking;
        public int luck;

        public Status(int hp, int attack, int defense, int hacking, int secure, int speed, int luck)
        {
            this.maxHP = hp;
            this.attack = attack;
            this.defense = defense;
            this.hacking = hacking;
            this.secure = secure;
            this.speed = speed;
            this.luck = luck;
        }

        public Status() : this(0, 0, 0, 0, 0, 0, 0) { }

        public static Status operator +(Status a, Status b)
        {
            return new Status(a.maxHP + b.maxHP, a.attack + b.attack, a.defense + b.defense, a.hacking + b.hacking, a.secure + b.secure, a.speed + b.speed, a.luck + b.luck);
        }

        public static Status operator -(Status a, Status b)
        {
            return new Status(a.maxHP - b.maxHP, a.attack - b.attack, a.defense - b.defense, a.hacking - b.hacking, a.secure - b.secure, a.speed - b.speed, a.luck - b.luck);
        }

        public static Status operator *(Status a, Status b)
        {
            return new Status(a.maxHP * b.maxHP, a.attack * b.attack, a.defense * b.defense, a.hacking * b.hacking, a.secure * b.secure, a.speed * b.speed, a.luck * b.luck);
        }

        public static Status one
        {
            get
            {
                return new Status(1, 1, 1, 1, 1, 1, 1);
            }
        }

        public override string ToString()
        {
            return "HP: " + maxHP +
                "\natt: " + attack +
                "\ndef: " + defense +
                "\nhac: " + hacking +
                "\nsec: " + secure +
                "\nspd: " + speed +
                "\nlck: " + luck;
        }
    }
}