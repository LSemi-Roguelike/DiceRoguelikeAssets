using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class SelfEffect : PassiveSkill
{
    [SerializeField] Effect _effect;
    protected override IEnumerator Cast()
    {
        _container.GetEffect(_effect);
        Debug.Log("self effect");
        yield return new WaitForSeconds(1f);
    }
}
