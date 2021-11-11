using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    protected Sprite sprite;

    public Sprite GetSprite() { return sprite; }
}
