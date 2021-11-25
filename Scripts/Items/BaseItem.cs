using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    [SerializeField] protected string id;
    [SerializeField] protected Sprite sprite;

    public string GetID() { return id; }
    public Sprite GetSprite() { return sprite; }
}
