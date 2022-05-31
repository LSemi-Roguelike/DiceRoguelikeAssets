using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class Main_1 : MainSkill
{
    [SerializeField] GameObject effectPrefab;

    public override IEnumerator Cast(BaseUnit caster,BaseContainer target)
    {
        Debug.Log(target.Name + " Main_1");
        Instantiate(effectPrefab, target.Pos, effectPrefab.transform.rotation);
        //StartCoroutine();
        yield break;
    }
}
