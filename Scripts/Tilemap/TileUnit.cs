using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public abstract class TileUnit : TileObject
{
    public enum UnitState { IDLE, TURN, MOVE, ATTACK }

    [SerializeField] protected GameObject rangePrefab;

    [SerializeField] protected float movementSpeed = 1.0f;
    [SerializeField] protected bool crossMove;
    
    protected bool myTurn;
    protected bool onCasting;
    protected Unit unit;

    protected int turnCost;
    protected int movePoint;
    protected SkillBase skill;
    protected UnitState unitState;

    protected List<Vector3> moveRoute;
    protected TileObject[] targetObjs;


    protected GameObject rangeContainer;
    protected GameObject[] inRange;
    protected Route[] rangeRoutes;

    protected override void Init()
    {
        base.Init();
        unit = mainObj.GetComponent<Unit>();
        if (unit == null)
            Debug.LogError(name + " has not unit object");

        rangeContainer = new GameObject();
        rangeContainer.transform.SetParent(transform);

        moveRoute = new List<Vector3>();
        inRange = new GameObject[] { };
        rangeRoutes = new Route[] { };

        unitState = UnitState.IDLE;
    }
    private void Start()
    {
        Init();
    }
    void Update()
    {
        PreUpdate();
        if (onCasting)
            return;
        switch (unitState)
        {
            case UnitState.IDLE:
                IdleUpdate();
                break;
            case UnitState.TURN:
                TurnUpdate();
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
                            StartCoroutine(SkillCasting(cellPos, unit.MoveAct()));
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

    public abstract void GetTurn();
    protected abstract void PreUpdate();
    protected abstract void IdleUpdate();
    protected abstract void TurnUpdate();
    protected abstract void MoveUpdate();
    protected abstract void AttackUpdate();

    protected IEnumerator SkillCasting(Vector3Int rootPos, List<SkillBase> skills)
    {
        if (skills == null)
            yield break;
        onCasting = true;
        foreach (var skill in skills)
        {
            TileObject[] targets;
            var routes = TileMapManager.manager.GetAttackableTiles(rootPos, skill.GetRange(), out targets);
            SetRange(routes);
            yield return skill.Cast(unit, TileObjArrToMainObjArr(targets));
        }
        SetIdle();
        onCasting = false;
    }
    protected bool MoveTo(Vector3Int pos)
    {
        for (int i = 0; i < rangeRoutes.Length; i++)
        {
            if (rangeRoutes[i].pos == pos)
            {
                Route destination = rangeRoutes[i];
                while (destination.preRoute != null)
                {
                    moveRoute.Insert(0, TileMapManager.manager.CellToWorld(destination.pos));
                    destination = destination.preRoute;
                }
                RemoveRnage();
                return true;
            }
        }
        return false;
    }

    protected IEnumerator AttackTo(Vector3Int pos)
    {
        for (int i = 0; i < rangeRoutes.Length; i++)
        {
            if (rangeRoutes[i].pos == pos)
            {
                TileObject[] targets;
                var routes = TileMapManager.manager.GetAttackableTiles(pos, skill.GetEffect(), out targets);
                SetRange(routes);
                yield return skill.Cast(unit, TileObjArrToMainObjArr(targets));
                RemoveRnage();

                yield return SkillCasting(pos, unit.AttackAct());
                break;
            }
        }
    }

    protected void SetIdle()
    {
        if (unitState == UnitState.IDLE)
            return;

        unitState = UnitState.IDLE;
        RemoveRnage();
        skill = null;
        movePoint = 0;
    }

    protected void SetTurn(int cost)
    {
        if (unitState == UnitState.TURN)
            return;

        turnCost = cost;
        unitState = UnitState.TURN;
        RemoveRnage();
        skill = null;
        movePoint = 0;
    }

    protected void SetMove(int movePoint)
    {
        if (unitState == UnitState.MOVE)
            return;

        unitState = UnitState.MOVE;

        RemoveRnage();
        SetRange(TileMapManager.manager.GetMoveableTiles(cellPos, movePoint, crossMove));
    }

    protected void SetSkill(SkillBase skill)
    {
        if (unitState == UnitState.ATTACK)
            return;

        unitState = UnitState.ATTACK;
        this.skill = skill;

        RemoveRnage();
        SetRange(TileMapManager.manager.GetAttackableTiles(cellPos, skill.GetRange(), out targetObjs));
        foreach (var target in targetObjs)
        {
            Debug.Log(target.name + target.transform.position);
        }
    }

    protected void SetRange(Route[] routes)
    {
        if (inRange.Length != 0)
            RemoveRnage();

        rangeRoutes = routes;
        inRange = new GameObject[routes.Length];
        for(int i = 0; i < routes.Length; i++)
        {
            GameObject temp = Instantiate(rangePrefab, TileMapManager.manager.CellToWorld(routes[i].pos), rangePrefab.transform.rotation);
            temp.transform.SetParent(rangeContainer.transform);
            inRange[i] = temp;
        }
    }

    protected void RemoveRnage()
    {
        foreach(var r in inRange)
        {
            Destroy(r);
        }
        inRange = new GameObject[] { };
    }
}
