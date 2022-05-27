using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class Sub_1 : SubSkill
{
    public override IEnumerator Cast(BaseContainer owner, float power)
    {
        Debug.Log(owner.Name + " Sub_1");

        yield return new WaitForSeconds(0.5f);
    }
}
