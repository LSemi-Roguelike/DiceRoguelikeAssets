using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillBase : MonoBehaviour
{
    public Range range;
    protected Character caster;
    public void SetCaster(Character caster){ this.caster = caster; }
    public virtual void Cast(Character target) { return; }
}