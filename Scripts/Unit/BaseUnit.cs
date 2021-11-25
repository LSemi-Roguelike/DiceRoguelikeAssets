using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected string charName;
    public string GetName() { return charName; }
    public string GetID() { return id; }

    protected void OnDestroy()
    {
        transform.parent?.GetComponent<TileObject>()?.DestroyObject();
    }
}