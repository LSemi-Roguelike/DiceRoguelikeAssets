using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : TileUnit
{
    //for test
    [SerializeField] protected int speed = 2;
    [SerializeField] protected GameObject attEffect;

    [SerializeField] Range myAttackRange;

    private void Start()
    {
        attRange = myAttackRange;
        Init();
    }

    protected override void PreUpdate()
    {
        if (canSelect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Tilemap")))
            {
                onMouse.SetActive(true);
                onMouse.transform.position = hit.transform.position + new Vector3(0, hit.transform.localScale.y / 2, 0);
            }
            else
            {
                onMouse.SetActive(false);
            }
        }

        TestUpdate();
    }


    protected override void IdleUpdate()
    {

    }

    protected override void MoveUpdate()
    {
        if (!Input.anyKey)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int clickCellPos = TileMapManager.manager.WorldToCell(onMouse.transform.position);
            MoveTo(clickCellPos);
        }
    }

    protected override void AttackUpdate()
    {
        if (!Input.anyKey)
            return;

        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void TestUpdate()
    {
        if (!Input.anyKey || moveRoute.Count > 0)
            return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetIdle();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movePoint = speed;
            SetMove();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetAttack();
        }
    }
}
