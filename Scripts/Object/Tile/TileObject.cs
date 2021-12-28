using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class TileObject : MonoBehaviour, IHaveInfo
    {
        [SerializeField] public string _id;
        [SerializeField] public string _name;
        [HideInInspector] public Vector3Int cellPos;

        public string Name { get { return _name; } }
        public string ID { get { return _id; } }

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