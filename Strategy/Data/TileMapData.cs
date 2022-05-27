using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    [System.Serializable]
    public struct ObjWithPos
    {
        public uint objID;
        public Vector3Int pos;

        public ObjWithPos(uint id, Vector3Int pos)
        {
            this.objID = id;
            this.pos = pos;
        }
    }


    [CreateAssetMenu(menuName = "Data/Mapdata", fileName = "Mapdata", order = 0)]
    public class TileMapData : ScriptableObject
    {
        public List<ObjWithPos> tiles;
        public List<ObjWithPos> units;
    }
}