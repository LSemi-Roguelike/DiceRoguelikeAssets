using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public abstract class TileObject : MonoBehaviour
{

    [SerializeField] protected BaseUnit baseUnit;
    protected Vector3Int cellPos;

    public BaseUnit GetMainUnit() { return baseUnit; }

    public virtual void Init()
    {
        cellPos = TileMapManager.manager.WorldToCell(transform.position);
        baseUnit.transform.SetParent(transform);
        transform.position = TileMapManager.manager.CellToWorld(cellPos);
    }

    public static BaseUnit[] TileObjArrToMainObjArr(TileObject[] tileObjects)
    {
        BaseUnit[] units = new BaseUnit[tileObjects.Length];
        for (int i = 0; i < tileObjects.Length; i++)
        {
            units[i] = tileObjects[i].GetMainUnit();
        }
        return units;
    }

    public virtual void DestroyObject()
    {
        TileMapManager.manager.RemoveUnit(cellPos);
        Destroy(gameObject);
    }
}
