using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    public class GeneralResourceManager : MonoBehaviour
    {
        public static GeneralResourceManager manager = null;

        [SerializeField] private List<BaseUnit> units;
        //[SerializeField] private List<BaseItem> items;

        private void Awake()
        {
            if (manager != null)
            {
                Destroy(gameObject);
            }
            manager = this;
        }

        public static BaseUnit GetUnitByID(uint id)
        {
            return manager.units.Find((x) => { return x.ID == id; });
        }

        //public static BaseItem GetItemByID(uint id)
        //{
        //    return manager.items.Find((x) => { return x.ID == id; });
        //}
    }
}