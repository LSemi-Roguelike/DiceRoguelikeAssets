using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class Sub_1 : SubSkill
{
    protected override IEnumerator Cast()
    {
        Debug.Log(_caster.Name + " Sub_1");

        yield break;
    }
}
