using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TileUnit : TileObject, IAttackable
{
    public enum UnitBehavior { IDLE, MOVE, ATTACK, END }

    [SerializeField] protected GameObject rangePrefab;
    [HideInInspector]
    public Unit unit;
    protected int turnPoint;
    [HideInInspector] public int turnCount;

    protected GameObject rangeContainer;
    protected GameObject[] rangeObjects;
    protected Route[] rangeRoutes;

    public override void Init()
    {
        base.Init();
        if (!(baseUnit is Unit))
        {
            Debug.LogError(name + " has not unit object");
            Destroy(gameObject);
        }
        unit = baseUnit as Unit;
        unit.Init();

        rangeContainer = new GameObject();
        rangeContainer.transform.SetParent(transform);

        rangeObjects = new GameObject[] { };
        rangeRoutes = new Route[] { };
        ResetTurnCount();
    }

    void ResetTurnCount()
    {
        turnCount = Utils.GetTurnCount(unit.GetStatus().speed);
    }

    protected abstract IEnumerator TurnStart();
    protected abstract IEnumerator TurnEnd();
    protected abstract IEnumerator BehaviorSelect(System.Action<UnitBehavior> action);
    protected abstract IEnumerator SelectMovement(System.Action<Movement> action);
    protected abstract IEnumerator SelectSkill(System.Action<Skill> action);
    protected abstract IEnumerator MovePointSelect(System.Action<int> action);
    protected abstract IEnumerator AttackTargetSelect(System.Action<int> action);

    public IEnumerator GetTurn()
    {
        yield return StartCoroutine(TurnStart());
        turnPoint = unit.GetTurnPoint();

        while (turnPoint > 0)
        {
            UnitBehavior unitState = UnitBehavior.IDLE;
            while (unitState == UnitBehavior.IDLE)
            {
                yield return StartCoroutine(BehaviorSelect((t) => { unitState = t; }));
                yield return null;
            }

            switch (unitState)
            {
                case UnitBehavior.MOVE:
                    yield return StartCoroutine(MoveUpdate());
                    break;
                case UnitBehavior.ATTACK:
                    yield return StartCoroutine(AttackUpdate());
                    break;
                case UnitBehavior.END:
                    turnPoint = -1;
                    break;
                default:
                    break;
            }
            yield return null;
        }
        yield return StartCoroutine(TurnEnd());
        ResetTurnCount();
    }

    protected IEnumerator SkillCasting(TileObject target, List<SkillBase> skills)
    {
        if (skills == null)
            yield break;

        foreach (var skill in skills)
        {
            yield return skill.Cast(unit, target);
        }
    }
    
    protected IEnumerator MoveUpdate()
    {
        //set move range
        RemoveRnage();
        Movement movement = null;
        yield return StartCoroutine(SelectMovement((a) => { movement = a; }));
        if (movement == null || movement.cost > turnPoint)
            yield break;

        SetRange(TileMapManager.manager.GetMoveableTiles(cellPos, movement.movePoint, movement.diagonalMove));

        //select move point
        turnPoint -= movement.cost;
        int select = -1;
        while (select < 0 || rangeRoutes.Length <= select)
        {
            yield return StartCoroutine(MovePointSelect((t) => { select = t; }));
            if (select == -2)
                yield break;

            yield return null;
        }

        //set move route
        List<Vector3> moveRoute = new List<Vector3>();
        Route destination = rangeRoutes[select];
        while (destination.preRoute != null)
        {
            moveRoute.Insert(0, TileMapManager.manager.CellToWorld(destination.pos));
            destination = destination.preRoute;
        }
        RemoveRnage();

        //on moving
        while (moveRoute.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveRoute[0], Utils.tileMoveSpeed * Time.deltaTime);
            if (transform.position == moveRoute[0])
            {
                Vector3Int temp = TileMapManager.manager.WorldToCell(moveRoute[0]);
                TileMapManager.manager.MoveUnit(cellPos, temp);
                cellPos = temp;
                moveRoute.RemoveAt(0);
            }
            yield return null;
        }
        //yield return SkillCasting(this, unit.MoveAct());
    }

    protected IEnumerator AttackUpdate()
    {
        //set attack skill
        RemoveRnage();
        Skill skill = null;
        yield return StartCoroutine(SelectSkill((t) => { skill = t; }));

        if (skill == null)
            yield break;

        TileObject[] targets;
        SetRange(TileMapManager.manager.GetAttackableTiles(cellPos, skill.range, out targets));
        if(targets.Length == 0)
            yield break;
        turnPoint -= skill.cost;

        var temp = new Route[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            temp[i] = rangeRoutes[i];
        }
        rangeRoutes = temp;

        //select target
        int select = -1;
        while (select < 0 || targets.Length <= select)
        {
            yield return StartCoroutine(AttackTargetSelect((t) => { select = t; }));
            if (select == -2)
                yield break;

            yield return null;
        }
        RemoveRnage();

        //cast skills
        yield return StartCoroutine(skill.skill.Cast(unit, targets[select]));
        //yield return StartCoroutine(SkillCasting(targets[select], unit.AttackAct()));
    }

    protected void SetRange(Route[] routes)
    {
        if (rangeObjects.Length != 0)
            RemoveRnage();

        rangeRoutes = routes;
        rangeObjects = new GameObject[routes.Length];
        for(int i = 0; i < routes.Length; i++)
        {
            GameObject temp = Instantiate(rangePrefab, TileMapManager.manager.CellToWorld(routes[i].pos), rangePrefab.transform.rotation);
            temp.transform.SetParent(rangeContainer.transform);
            rangeObjects[i] = temp;
        }
    }

    protected void RemoveRnage()
    {
        foreach(var r in rangeObjects)
        {
            Destroy(r);
        }
        rangeObjects = new GameObject[] { };
    }

    public override void DestroyObject()
    {
        TurnManager.manager.RemoveUnit(this);
        base.DestroyObject();
    }

    public abstract void GetAttack(Damage damage);
}
