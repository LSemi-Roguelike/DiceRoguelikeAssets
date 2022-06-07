using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace LSemiRoguelike.Strategy
{
    public abstract class StrategyContainer : BaseContainer
    {
        public Vector3Int cellPos { get; protected set; }

        public override void Init()
        {
            base.Init();
            cellPos = TileMapManager.manager.WorldToCell(Pos);
            transform.position = TileMapManager.manager.CellToWorld(cellPos);
        }

        protected virtual void OnDestroy()
        {
            TileMapManager.manager.RemoveUnit(this);
        }
    }
}