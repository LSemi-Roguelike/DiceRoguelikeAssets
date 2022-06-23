using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager manager;
        List<StrategyActingUnit> units;
        StrategyActingUnit turnUnit;
        Coroutine nowCo;

        private void Awake()
        {
            if (manager != null)
            {
                Destroy(gameObject);
                return;
            }
            manager = this;
        }

        public void Init()
        {
            units = new List<StrategyActingUnit>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var temp = transform.GetChild(i).GetComponent<StrategyActingUnit>();
                if (temp != null)
                    units.Add(temp);
            }

            if(units.Count == 0)
            {
                //TODO: none unit exeption
                Debug.LogError("no unit in filed");
            }

            units.Sort((x, y) =>
            {
                if (x.turnCount > y.turnCount)
                    return 1;
                else
                    return -1;
            });
            GiveTurn();

            //StartCoroutine(TurnManagingCo());
        }

        public void TurnRotate()
        {
            if (turnUnit.OnTurn)
            {
                return;
            }
            int i = 0;
            for (; i < units.Count && units[i].turnCount <= turnUnit.turnCount; i++) ;
            units.Insert(i, turnUnit);
            GiveTurn();
        }

        public void GiveQuickTurn(StrategyActingUnit nextUnit)
        {
            nextUnit.turnCount = 0;
            units.Remove(nextUnit);
            units.Insert(0, nextUnit);
            GiveTurn();
        }

        private void GiveTurn()
        {
            turnUnit = units[0];
            units.RemoveAt(0);
            foreach (var unit in units)
            {
                unit.turnCount -= turnUnit.turnCount;
            }
            turnUnit.turnCount = 0;
            ViewPointMove.Inst.MoveTo(turnUnit.transform);
            nowCo = StartCoroutine(turnUnit.GetTurn());
        }

        IEnumerator TurnManagingCo()
        {
            while (units != null)
            {
                var unit = units[0];
                int count = unit.turnCount;
                ViewPointMove.Inst.MoveTo(unit.transform);
                nowCo = StartCoroutine(unit.GetTurn());
                yield return nowCo;
                Debug.Log("end");
                ViewPointMove.Inst.ResetParent();

                units.Remove(unit);
                int insertPoint = 0;
                for (int i = 0; i < units.Count; i++)
                {
                    units[i].turnCount -= count;
                    if (units[i].turnCount <= unit.turnCount)
                        insertPoint++;
                }
                {
                    units.Insert(insertPoint, unit);
                }
                yield return null;
            }
            ViewPointMove.Inst?.ResetParent();
        }

        public void RemoveUnit(StrategyActingUnit unit)
        {
            if (unit == units![0])
            {
                ViewPointMove.Inst?.ResetParent();
            }
            units.Remove(unit);
        }
    }
}