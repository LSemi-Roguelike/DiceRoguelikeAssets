using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parts", menuName = "Items/Parts", order = 10)]
public class Parts : Item
{
    public enum PartsType { ARM, LEG, ARMOR }

    [SerializeField]
    PartsType partsType;

    private void OnEnable()
    {
        itemType = ItemType.PARTS;
    }
}
