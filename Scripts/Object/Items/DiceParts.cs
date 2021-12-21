using UnityEngine;


namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "DiceParts", menuName = "Items/DiceParts", order = 20)]
    public class DiceParts : BaseItem
    {
        protected Dice _dice;
        protected int _cost;
        [SerializeField] protected MainSkill _diceSkill = null;

        public Dice dice { get { return _dice; } set { _dice = value; } }
        public int cost { get { return _cost; } set { _cost = value; } }
        public MainSkill DiceSkill { get { return _diceSkill; } }
    }
}