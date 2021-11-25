using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SkillBase : MonoBehaviour
{
    public abstract IEnumerator Cast(Unit caster, TileObject[] targets);
    public IEnumerator Cast(Unit caster, TileObject target)
    {
        yield return Cast(caster, new TileObject[] { target });
    }
}