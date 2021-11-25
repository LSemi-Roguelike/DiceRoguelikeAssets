using UnityEngine;

public abstract class DiceParts : BaseItem
{
    protected Dice dice;

    public void SetDice(Dice dice)
    {
        this.dice = dice;
    }
}
