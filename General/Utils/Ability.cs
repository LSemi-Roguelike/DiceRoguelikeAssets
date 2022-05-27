namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Ability
    {
        public int attack;
        public int defense;
        public int secure;
        public int speed;
        public int hacking;
        public int luck;

        public Ability(int attack, int defense, int hacking, int secure, int speed, int luck)
        {
            this.attack = attack;
            this.defense = defense;
            this.hacking = hacking;
            this.secure = secure;
            this.speed = speed;
            this.luck = luck;
        }

        public static Ability operator +(Ability a, Ability b)
        {
            return new Ability(a.attack + b.attack, a.defense + b.defense, a.hacking + b.hacking, a.secure + b.secure, a.speed + b.speed, a.luck + b.luck);
        }

        public static Ability operator -(Ability a, Ability b)
        {
            return new Ability(a.attack - b.attack, a.defense - b.defense, a.hacking - b.hacking, a.secure - b.secure, a.speed - b.speed, a.luck - b.luck);
        }

        public static Ability operator *(Ability a, Ability b)
        {
            return new Ability(a.attack * b.attack, a.defense * b.defense, a.hacking * b.hacking, a.secure * b.secure, a.speed * b.speed, a.luck * b.luck);
        }

        public static Ability one => new Ability(1, 1, 1, 1, 1, 1);

        public override string ToString()
        {
            return "att: " + attack +
                "\ndef: " + defense +
                "\nhac: " + hacking +
                "\nsec: " + secure +
                "\nspd: " + speed +
                "\nlck: " + luck;
        }
    }
}