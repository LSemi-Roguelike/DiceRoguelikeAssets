using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;
public class DirectAttack : MainSkill
{
    [SerializeField] private Effect effect;
    [SerializeField] private GameObject vfxPrefab;
    [SerializeField] private AudioClip sfxClip;
    public override IEnumerator Cast(BaseUnit caster, BaseContainer target)
    {
        yield break;
    }
}
