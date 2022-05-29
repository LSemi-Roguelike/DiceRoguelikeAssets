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
        var finalEffect = effect;
        finalEffect = caster.SetEffect(finalEffect);
        Instantiate(vfxPrefab, target.Pos, vfxPrefab.transform.rotation);
        target.GetEffect(finalEffect);
        yield break;
    }
}
