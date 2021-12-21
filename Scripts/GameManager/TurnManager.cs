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
                int count = units[0].turnCount;
                float cameraSpeed = (cameraViewPoint.transform.position - units[0].transform.position).magnitude * (1 / Utils.cameraMoveTime);
                while (cameraViewPoint.transform.position != units[0].transform.position)
                {
                    cameraViewPoint.position = Vector3.MoveTowards(cameraViewPoint.position, units[0].transform.position, cameraSpeed * Time.deltaTime);
                    yield return null;
                }

                cameraViewPoint.SetParent(units[0].transform);
                yield return StartCoroutine(units[0].GetTurn());
                cameraViewPoint.SetParent(null);
                int insertPoint = 1;

                for (int i = 1; i < units.Count; i++)
                {
                    units[i].turnCount -= count;
                    if (units[i].turnCount <= units[0].turnCount)
                        insertPoint++;
                }
                units.Insert(insertPoint, units[0]);
                units.RemoveAt(0);

                yield return null;
            }
        }

        public void RemoveUnit(TileUnit unit)
        {
            units.Remove(unit);
        }
    }
}