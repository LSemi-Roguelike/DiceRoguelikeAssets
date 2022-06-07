using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    public abstract class StrategyUnit : StrategyContainer
    {
        protected enum ActType { WaitAction, SelectAction, SelectTarget, TurnEnd }
        protected ActType nowAct;

        [HideInInspector] public int turnCount;
        public new ActingUnit unit { get { return base.unit as ActingUnit; } }
        
        protected int turnPoint;
        protected bool _onTurn;
        public bool OnTurn => _onTurn;
        public bool isDead => unit.IsDead;

        public override void Init()
        {
            base.Init();
            ResetTurnCount();
        }


        protected abstract void TurnStart();
        protected abstract void TurnEnd();
        protected abstract void TurnUpdate();

        public IEnumerator GetTurn()
        {
            _onTurn = true;
            TurnStart();
            while (OnTurn)
            {
                TurnUpdate();
                yield return null;
            }
            TurnEnd();
            ResetTurnCount();
            TurnManager.manager.TurnRotate();
        }

        protected bool Acting(StrategyAction action, Vector3Int targetPos)
        {
            if (action.skill is Move)
            {
                var rangeRoutes = action.routes;
                Route targetRoute = null;
                for (int i = 0; i < rangeRoutes.Length; i++)
                {
                    if (rangeRoutes[i].pos == targetPos)
                    {
                        targetRoute = rangeRoutes[i];
                    }
                }
                if (targetRoute == null)
                    return false;

                //set move route
                List<Vector3> moveRoute = new List<Vector3>();
                while (targetRoute.preRoute != null)
                {
                    moveRoute.Insert(0, TileMapManager.manager.CellToWorld(targetRoute.pos));
                    targetRoute = targetRoute.preRoute;
                }
                nowAct = ActType.WaitAction;
                StartCoroutine(MoveTo(moveRoute));
                return true;
            }
            else
            {
                var targets = action.targets;
                foreach (var target in targets)
                {
                    if (target.cellPos == targetPos)
                    {
                        nowAct = ActType.WaitAction;
                        StartCoroutine(SkillCast(action.skill, target));
                        return true;
                    }
                }
                return false;
            }
        }

        protected IEnumerator SkillCast(MainSkill skill, StrategyContainer target)
        {
            yield return StartCoroutine(skill.Cast(target));
            nowAct = ActType.SelectAction;
            _statusUI.SetUI(_unit.status);
        }

        protected IEnumerator MoveTo(List<Vector3> moveRoute)
        {
            //on moving
            int moveCount = 0;
            while (moveRoute.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveRoute[0], Utils.tileMoveSpeed * Time.deltaTime);
                if (transform.position == moveRoute[0])
                {
                    Vector3Int temp = TileMapManager.manager.WorldToCell(moveRoute[0]);
                    TileMapManager.manager.MoveUnit(cellPos, temp);
                    cellPos = temp;
                    moveRoute.RemoveAt(0);
                    moveCount++;
                }
                yield return null;
            }
            nowAct = ActType.SelectAction;
            _statusUI.SetUI(_unit.status);
        }
        protected void ResetTurnCount()
        {
            turnCount = 10;
            //turnCount = Utils.GetTurnCount(unit.totalStatus.power);
        }

        protected void Death()
        {
            unit.gameObject.GetComponent<Renderer>().enabled = false;
            TurnManager.manager.RemoveUnit(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}