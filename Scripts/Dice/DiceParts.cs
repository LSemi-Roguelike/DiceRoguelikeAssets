using UnityEngine;

[CreateAssetMenu(fileName = "DiceParts", menuName = "Items/DiceParts", order = 30)]
public class DiceParts : Item
{
    [SerializeField]
    int cost;
    [SerializeField]
    DiceType diceType;
    public DiceType GetDiceType() { return diceType; }

    private void OnEnable()
    {
        itemType = ItemType.DICEPARTS;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public void Init(DiceParts item, Character owner)
    {
        diceType = item.diceType;
        base.Init(item, owner);
    }
}
