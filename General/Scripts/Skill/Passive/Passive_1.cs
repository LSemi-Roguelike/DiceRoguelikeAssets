using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class Passive_1 : PassiveSkill
{
    protected override IEnumerator Cast()
    {
        Debug.Log(_caster.Name + " Passive_1");

        yield break;
    }
}
