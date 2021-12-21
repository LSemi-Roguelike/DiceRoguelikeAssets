using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace LSemiRoguelike
{
    public abstract class TileContainer : BaseContainer
    {
        public Vector3Int cellPos { get; protected set; }

        protected override void Init()
        {
            base.Init();
            _unit.Init();
            cellPos = TileMapManager.manager.WorldToCell(transform.position);
            transform.position = TileMapManager.manager.CellToWorld(cellPos);
        }

        public virtual void DestroyObject()
        {
            TileMapManager.manager.RemoveUnit(cellPos);
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            TileMapManager.manager.RemoveUnit(cellPos);
        }
    }
}