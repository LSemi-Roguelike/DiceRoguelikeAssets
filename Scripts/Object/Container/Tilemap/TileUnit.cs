using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    public abstract class TileUnit : TileContainer, IDamageable
    {
        public enum UnitBehavior { IDLE, MOVE, ATTACK, END }

        [SerializeField] protected GameObject rangePrefab;
        protected int turnPoint;
        [HideInInspector] public int turnCount;
        public ActorUnit actUnit { get { return unit as ActorUnit; } }
        protected GameObject rangeContainer;
        protected GameObject[] rangeObjects;
        protected Route[] rangeRoutes;
        protected bool diagonalMove;

        protected override void Init()
        {
            base.Init();

            rangeContainer = new GameObject("Range Container");
            rangeContainer.transform.SetParent(transform);
            rangeContainer.transform.localPosition = Vector3.zero;

            rangeObjects = new GameObject[] { };
            rangeRoutes = new Route[] { };
            ResetTurnCount();
        }

        void ResetTurnCount()
        {
            turnCount = Utils.GetTurnCount(unit.totalStatus.speed);
        }

        protected abstract IEnumerator TurnStart();
        protected abstract IEnumerator TurnEnd();
        protected abstract IEnumerator BehaviorSelect(System.Action<UnitBehavior> action);
        protected abstract IEnumerator SelectMovement(System.Action<int> action);
        protected abstract IEnumerator SelectSkill(System.Action<MainSkill> action);
        protected abstract IEnumerator MovePointSelect(System.Action<int> action);
        protected abstract IEnumerator AttackTargetSelect(System.Action<int> action);
        protected abstract IEnumerator MoveEnd(int movement);
        protected abstract IEnumerator AttackEnd(TileContainer target);
        protected abstract IEnumerator GetDamageEnd(Damage damage);

        public IEnumerator GetTurn()
        {
            turnPoint = (unit as ActorUnit).GetTurnPoint();
            yield return StartCoroutine(TurnStart());

            while (turnPoint > 0)
            {
                UnitBehavior unitState = UnitBehavior.IDLE;
                while (unitState == UnitBehavior.IDLE)
                {
                    yield return StartCoroutine(BehaviorSelect((t) => { unitState = t; }));
                    if (unitState == UnitBehavior.END)
                    {
                        yield return StartCoroutine(TurnEnd());
                        yield break;
                    }
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
                RemoveRnage();
                yield return null;
            }
            yield return StartCoroutine(TurnEnd());
            ResetTurnCount();
        }

        protected IEnumerator SkillCasting(BaseSkill skill, TileContainer target = null)
        {
            if (target == null)
                target = this;

            yield return StartCoroutine(skill.Cast(target));
        }

        protected IEnumerator MoveUpdate()
        {
            //set move range
            RemoveRnage();
            int movement = -1;
            yield return StartCoroutine(SelectMovement((a) => { movement = a; }));
            if (movement == -1)
                yield break;

            SetRange(TileMapManager.manager.GetMoveableTiles(cellPos, movement, diagonalMove));

            //select move point
            turnPoint -= 1;
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
            yield return StartCoroutine(MoveEnd(movement));
        }

        protected IEnumerator AttackUpdate()
        {
            //set attack skill
            RemoveRnage();
            MainSkill skill = null;
            yield return StartCoroutine(SelectSkill((t) => { skill = t; }));

            if (skill == null)
                yield break;

            TileContainer[] targets;
            SetRange(TileMapManager.manager.GetAttackableTiles(cellPos, skill.range, out targets));
            if (targets.Length == 0)
                yield break;
            turnPoint -= 1;

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
            yield return StartCoroutine(skill.Cast(targets[select]));

            yield return StartCoroutine(AttackEnd(targets[select]));
        }
        
        public virtual IEnumerator GetDamage(Damage damage)
        {
            if(unit.GetDamage(damage))
            {
                Destroy(gameObject);
            }
            yield return StartCoroutine(GetDamageEnd(damage));
        }

        protected void SetRange(Route[] routes)
        {
            if (rangeObjects.Length != 0)
                RemoveRnage();

            rangeRoutes = routes;
            rangeObjects = new GameObject[routes.Length];
            for (int i = 0; i < routes.Length; i++)
            {
                GameObject temp = Instantiate(rangePrefab, TileMapManager.manager.CellToWorld(routes[i].pos), rangePrefab.transform.rotation);
                temp.transform.SetParent(rangeContainer.transform);
                rangeObjects[i] = temp;
            }
        }

        protected void RemoveRnage()
        {
            foreach (var r in rangeObjects)
            {
                Destroy(r);
            }
            rangeObjects = new GameObject[] { };
        }

        protected override void OnDestroy()
        {
            TurnManager.manager.RemoveUnit(this);
            base.OnDestroy();
        }
    }
}