using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    public class ListManager : MonoBehaviour
    {
        public static ListManager manager = null;
        [SerializeField] public List<BaseUnit> unitList;
        [SerializeField] public List<BaseItem> itemList;
        [SerializeField] public List<TileObject> tileList;

        private void Awake()
        {
            if (manager != null)
            {
                Destroy(gameObject);
            }
            manager = this;
        }
        public int UnitIdToNum(string id)
        {
            return unitList.FindIndex((x) => { return x.ID == id; });
        }
        public int ItemIdToNum(string id)
        {
            return itemList.FindIndex((x) => { return x.ID == id; });
        }
        public int TileIdToNum(string id)
        {
            return tileList.FindIndex((x) => { return x.ID == id; });
        }


        public BaseUnit GetUnitByID(string id)
        {
            return unitList.Find((x) => { return x.ID == id; });
        }

        public BaseUnit GetUnitByNum(int num)
        {
            if (unitList.Count <= num)
                return null;

            return unitList[num];
        }

        public BaseItem GetItemByID(string id)
        {
            return itemList.Find((x) => { return x.ID == id; });
        }

        public BaseItem GetItemByNum(int num)
        {
            if (itemList.Count <= num)
                return null;

            return itemList[num];
        }

        public TileObject GetTileByID(string id)
        {
            return tileList.Find((x) => { return x.ID == id; });
        }

        public TileObject GetTileByNum(int num)
        {
            if (tileList.Count <= num)
                return null;

            return tileList[num];
        }
    }
}