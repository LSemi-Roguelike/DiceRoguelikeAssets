[System.Serializable]
public class Status
{
    public int hp;
    public int attack;
    public int defense;
    public int hacking;
    public int secure;
    public int speed;
    public int luck;

    public Status(int hp, int attack, int defense, int hacking, int secure, int speed, int luck)
    {
        this.hp = hp;
        this.attack = attack;
        this.defense = defense;
        this.hacking = hacking;
        this.secure = secure;
        this.speed = speed;
        this.luck = luck;
    }

    public Status()
    {
        this.hp = 0;
        this.attack = 0;
        this.defense = 0;
        this.hacking = 0;
        this.secure = 0;
        this.speed = 0;
        this.luck = 0;
    }

    public static Status operator +(Status a, Status b)
    {
        return new Status(a.hp + b.hp, a.attack + b.attack, a.defense + b.defense, a.hacking + b.hacking, a.secure + b.secure,  a.speed + b.speed, a.luck + b.luck);
    }

    public static Status operator -(Status a, Status b)
    {
        return new Status(a.hp - b.hp, a.attack - b.attack, a.defense - b.defense, a.hacking - b.hacking, a.secure - b.secure, a.speed - b.speed, a.luck - b.luck);
    }

    public static Status operator *(Status a, Status b)
    {
        return new Status(a.hp * b.hp, a.attack * b.attack, a.defense * b.defense, a.hacking * b.hacking, a.secure * b.secure, a.speed * b.speed, a.luck * b.luck);
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
        return "HP: " + hp + 
            "\natt: " + attack + 
            "\ndef: " + defense + 
            "\nhac: " + hacking + 
            "\nsec: " + secure + 
            "\nspd: " + speed + 
            "\nlck: " + luck;
    }
}
