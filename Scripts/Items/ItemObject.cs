using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemObject : MonoBehaviour
{
    [SerializeField]
    Item item;

    private void Awake()
    {
        gameObject.tag = "Item";
        GetComponent<SpriteRenderer>().sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }
}
