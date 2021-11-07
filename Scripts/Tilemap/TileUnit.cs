using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public abstract class TileUnit : MonoBehaviour
{
    public enum UnitState { IDLE, MOVE, ATTACK }

    [SerializeField] protected GameObject rangePrefab;
    [SerializeField] protected GameObject onMouse;
    [SerializeField] protected float movementSpeed = 1.0f;
    [SerializeField] protected bool canSelect;
    [SerializeField] protected bool crossMove;

    protected int movePoint;
    protected Vector3Int cellPos;
    protected UnitState unitState;

    protected List<Vector3> moveRoute;

    protected Range attRange;

    protected List<GameObject> inRange;
    protected GameObject rangeContainer;
    protected List<Route> rangeRoutes;

    protected void Init()
    {
        cellPos = TileMapManager.manager.WorldToCell(transform.position);

        rangeContainer = Instantiate(new GameObject());
        rangeContainer.transform.SetParent(transform);

        moveRoute = new List<Vector3>();
        inRange = new List<GameObject>();
        rangeRoutes = new List<Route>();

        unitState = UnitState.IDLE;
    }
    private void Start()
    {
        Init();
    }
    void Update()
    {
        PreUpdate();
        switch (unitState)
        {
            case UnitState.IDLE:
                IdleUpdate();
                break;
            case UnitState.MOVE:
                if (moveRoute.Count > 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, moveRoute[0], movementSpeed * Time.deltaTime);
                    if (transform.position == moveRoute[0])
                    {
                        Vector3Int temp = TileMapManager.manager.WorldToCell(moveRoute[0]);
                        TileMapManager.manager.MoveUnit(cellPos, temp);
                        cellPos = temp;
                        moveRoute.RemoveAt(0);
                        movePoint--;
                        if (moveRoute.Count == 0)
                        {
                            if (movePoint == 0)
                                SetAttack();
                            else
                                SetMove();
                        }
                    }
                    break;
                }
                MoveUpdate();
                break;
            case UnitState.ATTACK:
                AttackUpdate();
                break;
        }
    }

    protected abstract void PreUpdate();
    protected abstract void IdleUpdate();
    protected abstract void MoveUpdate();
    protected abstract void AttackUpdate();

    protected void MoveTo(Vector3Int pos)
    {
        for (int i = 0; i < rangeRoutes.Count; i++)
        {
            if (rangeRoutes[i].pos == pos)
            {
                Route root = rangeRoutes[i];
                while (root.preRoute != null)
                {
                    moveRoute.Insert(0, TileMapManager.manager.CellToWorld(root.pos));
                    root = root.preRoute;
                }
                RemoveRnage();
                break;
            }
        }
    }

    protected bool MoveTo(Route root)
    {
        if (!rangeRoutes.Contains(root))
            return false;
        TileMapManager.manager.MoveUnit(cellPos, root.pos);
        cellPos = root.pos;
        while (root.preRoute != null)
        {
            moveRoute.Insert(0, TileMapManager.manager.CellToWorld(root.pos));
            root = root.preRoute;
        }
        return true;
    }

    protected void SetIdle()
    {
        if (unitState == UnitState.IDLE)
            return;

        unitState = UnitState.IDLE;
        RemoveRnage();
    }

    protected void SetMove()
    {
        if (unitState == UnitState.MOVE)
        {
            SetRange(TileMapManager.manager.GetMoveableTiles(cellPos, movePoint, crossMove));
            return;
        }

        unitState = UnitState.MOVE;
        RemoveRnage();

        SetRange(TileMapManager.manager.GetMoveableTiles(cellPos, movePoint, crossMove));
    }

    protected void SetAttack()
    {
        if (unitState == UnitState.ATTACK)
            return;

        unitState = UnitState.ATTACK;
        RemoveRnage();

        SetRange(TileMapManager.manager.GetAttackableTiles(cellPos, attRange));
    }

    protected void SetRange(List<Route> roots)
    {
        rangeRoutes = roots;
        foreach (var root in roots)
        {
            GameObject temp = Instantiate(rangePrefab, TileMapManager.manager.CellToWorld(root.pos), rangePrefab.transform.rotation);
            temp.transform.SetParent(rangeContainer.transform);
            inRange.Add(temp);
        }
    }

    protected void RemoveRnage()
    {
        foreach(var r in inRange)
        {
            Destroy(r);
        }
        inRange = new List<GameObject>();
    }


    public void GetForce(Vector3Int dir)
    {

    }
}
