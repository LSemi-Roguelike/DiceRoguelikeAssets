using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class RangeViewManager : MonoBehaviour
    {
        public static RangeViewManager Inst { get; private set; }
        [SerializeField] private GameObject rangePrefab;
        [SerializeField] private int startObjCount;
        private List<GameObject> rangeObjects;

        private void Awake()
        {
            if (Inst) Destroy(gameObject);
            Inst = this;

            rangeObjects = new List<GameObject>();
            for(int i = 0; i < startObjCount; i++)
                rangeObjects.Add(Instantiate(rangePrefab, transform));
        }

        public void ViewRange(Route[] routes)
        {
            for (int i = rangeObjects.Count; i < routes.Length; i++)
            {
                rangeObjects.Insert(0, Instantiate(rangePrefab, transform));
            }

            for (int i = 0; i < routes.Length; i++)
            {
                rangeObjects[i].SetActive(true);
                rangeObjects[i].transform.position = TileMapManager.manager.CellToWorld(routes[i].pos);
            }
        }

        public void HideRange()
        {
            rangeObjects.ForEach((o) => o.SetActive(false));
        }
    }
}