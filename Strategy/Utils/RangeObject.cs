using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;
using LSemiRoguelike.Strategy;

public class RangeObject : MonoBehaviour
{
    [SerializeField] GameObject rangeObjectPrefab;
    GameObject[] rangeObjects;
    public StrategyContainer[] targets;
    [HideInInspector] public Route[] routes;

    public void SetRange(Range range, StrategyContainer container)
    {
        if (rangeObjects != null)
        {
            foreach (var obj in rangeObjects)
            {
                Destroy(obj);
            }
        }

        transform.SetParent(container.transform);
        (routes, targets) = TileMapManager.manager.GetRangeTiles(container.cellPos, range);

        rangeObjects = new GameObject[routes.Length];
        for (int i = 0; i < routes.Length; i++)
        {
            rangeObjects[i] = Instantiate(rangeObjectPrefab, TileMapManager.manager.CellToWorld(routes[i].pos), rangeObjectPrefab.transform.rotation);
            rangeObjects[i].transform.SetParent(transform);
        }
        gameObject.SetActive(false);
    }

    public void ResetRange()
    {

    }
}
