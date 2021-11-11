using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemObject : MonoBehaviour
{
    [SerializeField]
    BaseItem obj;

    private void Awake()
    {
        gameObject.tag = "Item";
        GetComponent<SpriteRenderer>().sprite = obj.GetSprite();
    }

    public BaseItem GetItem()
    {
        return obj;
    }
}
