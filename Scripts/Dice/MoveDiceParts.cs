using UnityEngine;

[CreateAssetMenu(fileName = "MoveDiceParts", menuName = "Items/MoveDiceParts", order = 30)]
public class MoveDiceParts : DiceParts
{
    [SerializeField]
    Movement movement;

    public Movement getMovement { get { return movement; } }
}
