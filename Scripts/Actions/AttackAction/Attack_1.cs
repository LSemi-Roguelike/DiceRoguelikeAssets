using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_1 : SkillBase
{
    [SerializeField] GameObject effectPrefab;
    public void AttackAction(int dist)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Cast(Unit caster,  GameObject[] targets)
    {
        foreach (var target in targets)
        {
            Debug.Log(target.name + ", " + TileMapManager.manager.WorldToCell(target.transform.position));
            Instantiate(effectPrefab, target.transform.position, target.transform.rotation);
        }
        yield return new WaitForSeconds(1);
    }
}
