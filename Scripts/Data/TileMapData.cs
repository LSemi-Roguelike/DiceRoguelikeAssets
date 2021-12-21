using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public struct TileWithPos
    {
        public string objID;
        public Vector3Int pos;

        public TileWithPos(string id, Vector3Int pos)
        {
            this.objID = id;
            this.pos = pos;
        }
    }

    [System.Serializable]
    public struct UnitWithPos
    {
        public string objID;
        public string containerID;
        public Vector3Int pos;

        public UnitWithPos(string objID, string containerID, Vector3Int pos)
        {
            this.objID = objID;
            this.containerID = containerID;
            this.pos = pos;
        }
    }

    [CreateAssetMenu(menuName = "Data/Mapdata", fileName = "Mapdata", order = 0)]
    public class TileMapData : ScriptableObject
    {
        public List<TileWithPos> tiles;
        public List<UnitWithPos> units;
    }
}