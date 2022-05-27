using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Dice
    {
        [SerializeField] public int _maxCost;
        [SerializeField] private UnitAction up;
        [SerializeField] private UnitAction down;
        [SerializeField] private UnitAction left;
        [SerializeField] private UnitAction right;
        [SerializeField] private UnitAction forward;
        [SerializeField] private UnitAction back;

        public int MaxCost => _maxCost;
        public UnitAction[] unitActions => new UnitAction[] { right, up, forward, left, down, back };
    }
}