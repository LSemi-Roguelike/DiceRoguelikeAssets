using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public abstract class TileObject : MonoBehaviour
{

    [SerializeField] protected GameObject mainObj;
    protected Vector3Int cellPos;

    public GameObject GetMainObj() { return mainObj; }

    protected virtual void Init()
    {
        cellPos = TileMapManager.manager.WorldToCell(transform.position);
    }
    private void Start()
    {
        Init();
    }

    public void GetForce(Vector3Int dir)
    {

    }

    public static GameObject[] TileObjArrToMainObjArr(TileObject[] tileObjects)
    {
        GameObject[] units = new GameObject[tileObjects.Length];
        for (int i = 0; i < tileObjects.Length; i++)
        {
            units[i] = tileObjects[i].GetMainObj();
        }
        return units;
    }
}
