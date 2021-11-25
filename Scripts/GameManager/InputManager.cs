using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager manager;
    public GameObject mouseCell;

    private void Awake()
    {
        manager = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Tilemap")))
        {
            mouseCell.SetActive(true);
            mouseCell.transform.position = hit.transform.position + new Vector3(0, hit.transform.localScale.y / 2, 0);
        }
        else
        {
            mouseCell.SetActive(false);
        }
    }

    public Vector3Int GetMouseCellPos()
    {
        return TileMapManager.manager.WorldToCell(mouseCell.transform.position);
    }
}
