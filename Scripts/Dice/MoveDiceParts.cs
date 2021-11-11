using UnityEngine;

[CreateAssetMenu(fileName = "MoveDiceParts", menuName = "Items/MoveDiceParts", order = 30)]
public class MoveDiceParts : DiceParts
{
    [SerializeField]
    int movePoint;

    public int getMovePoint { get { return movePoint; } }
}
