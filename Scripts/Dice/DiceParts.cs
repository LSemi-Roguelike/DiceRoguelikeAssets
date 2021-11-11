using UnityEngine;

public abstract class DiceParts : BaseItem
{
    [SerializeField]
    int cost;

    public int getCost { get { return cost; } }
}
