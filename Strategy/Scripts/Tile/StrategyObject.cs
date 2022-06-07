using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyObject : MonoBehaviour, IHaveInfo
    {
        [SerializeField] public uint _id;
        [SerializeField] public string _name;
        [HideInInspector] public Vector3Int cellPos;

        public string Name => _name;
        public uint ID => _id;

        public void Init(Vector3Int pos)
        {
            cellPos = pos;
        }
        public void Init()
        {
            cellPos = TileMapManager.manager.WorldToCell(transform.position);
        }
    }
}