using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager manager;
        List<TileUnit> units;
        Transform cameraViewPoint;
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
            cameraViewPoint = GameObject.Find("CameraViewPoint").transform;
            units = new List<TileUnit>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var temp = transform.GetChild(i).GetComponent<TileUnit>();
                if (temp)
                    units.Add(temp);
            }

            units.Sort((x, y) =>
            {
                if (x.turnCount > y.turnCount)
                    return 1;
                else
                    return -1;
            });

            StartCoroutine(TurnManagingCo());
        }

        IEnumerator TurnManagingCo()
        {
            while (units != null)
            {
                var unit = units[0];
                int count = unit.turnCount;
                float cameraSpeed = (cameraViewPoint.transform.position - unit.transform.position).magnitude * (1 / Utils.cameraMoveTime);
                Transform pointedTransform = unit.transform;

                while (cameraViewPoint.transform.position != unit.transform.position)
                {
                    cameraViewPoint.position = Vector3.MoveTowards(cameraViewPoint.position, pointedTransform.position, cameraSpeed * Time.deltaTime);
                    yield return null;
                }

                cameraViewPoint.SetParent(pointedTransform);
                nowCo = StartCoroutine(unit.GetTurn());
                yield return nowCo;

                cameraViewPoint.SetParent(null);

                units.Remove(unit);
                int insertPoint = 0;
                for (int i = 0; i < units.Count; i++)
                {
                    units[i].turnCount -= count;
                    if (units[i].turnCount <= unit.turnCount)
                        insertPoint++;
                }
                units.Insert(insertPoint, unit);
                Debug.Log(units.Count);

                yield return null;
            }
        }

        public void RemoveUnit(TileUnit unit)
        {
            if (unit == units[0])
            {
                cameraViewPoint.SetParent(null);
                StopCoroutine(nowCo);
            }
            units.Remove(unit);
        }
    }
}