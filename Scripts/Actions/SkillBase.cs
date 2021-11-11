using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SkillBase : MonoBehaviour
{
    [SerializeField] protected Range range;
    [SerializeField] protected Range effect;

    public Range GetRange() { return range; }
    public Range GetEffect() { return effect; }
    public abstract IEnumerator Cast(Unit caster, GameObject[] targets);
}