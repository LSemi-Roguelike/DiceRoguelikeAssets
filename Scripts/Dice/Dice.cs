using UnityEngine;
public enum DiceType { MOVEMENT, BATTLE }

public class Dice : MonoBehaviour
{
    [SerializeField]
    int maxCost;
    [SerializeField]
    DiceType diceType;
    [SerializeField]
    public DiceParts[] sides { get; private set; }

    public DiceType GetDiceType(){return diceType;}

}
