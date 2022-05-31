using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager manager = null;

        [SerializeField] private List<StrategyObject> tiles;
        [SerializeField] private List<StrategyContainer> containers;

        private void Awake()
        {
            if (manager != null)
            {
                Destroy(gameObject);
            }
            manager = this;
        }


        //Tile
        public static StrategyObject GetTileByID(uint id)
        {
            Debug.Log(manager);
            return manager.tiles.Find((x) => { return x.ID == id; });
        }

        public static StrategyContainer GetContainerByID(uint id)
        {
            return manager.containers.Find((x) => { return x.ID == id; });
        }

    }
}