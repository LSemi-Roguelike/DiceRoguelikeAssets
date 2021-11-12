using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : TileUnit
{
    //for test
    [SerializeField] protected GameObject onMouse;
    [SerializeField] protected bool canSelect;

    private void Start()
    {
        Init();
    }

    public override void GetTurn()
    {

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

    protected override void TurnUpdate()
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
            Vector3Int clickCellPos = TileMapManager.manager.WorldToCell(onMouse.transform.position);
            StartCoroutine(AttackTo(clickCellPos));
        }
    }

    public void SelectDiceParts(DiceParts diceParts)
    {
        if (diceParts is MoveDiceParts)
        {
            var moveParts = diceParts as MoveDiceParts;
            SetMove(moveParts.getMovePoint);
        }
        else if (diceParts is SkillDiceParts)
        {
            var skillParts = diceParts as SkillDiceParts;
            SetSkill(skillParts.getDiceSkill);
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
            SetMove(unit.GetMovePoint());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetSkill(unit.GetAttackSkill());
        }
    }
}
