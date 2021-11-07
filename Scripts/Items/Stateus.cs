[System.Serializable]
public class Status
{
    public float hp;
    public float attack;
    public float defense;
    public float speed;

    public Status(float hp, float attack, float defense, float speed)
    {
        this.hp = hp;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }

    public Status()
    {
        this.hp = 0;
        this.attack = 0;
        this.defense = 0;
        this.speed = 0;
    }

    public static Status operator +(Status a, Status b)
    {
        return new Status(a.hp + b.hp, a.attack + b.attack, a.defense + b.defense, a.speed + b.speed);
    }

    public static Status operator -(Status a, Status b)
    {
        return new Status(a.hp - b.hp, a.attack - b.attack, a.defense - b.defense, a.speed - b.speed);
    }

    public static Status operator *(Status a, Status b)
    {
        return new Status(a.hp * b.hp, a.attack * b.attack, a.defense * b.defense, a.speed * b.speed);
    }

    public static Status one
    {
        get
        {
            return new Status(1, 1, 1, 1);
        }
    }

    public override string ToString()
    {
        return "HP: " + hp + ", att: " + attack + ", def: " + defense + ", spd: " + speed;
    }
}
