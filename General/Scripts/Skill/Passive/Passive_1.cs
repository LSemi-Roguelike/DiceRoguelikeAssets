using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class Passive_1 : PassiveSkill
{

    public override IEnumerator Cast(BaseContainer owner)
    {
        Debug.Log(owner.Name + " Passive_1");

        yield return new WaitForSeconds(1);
    }
}
