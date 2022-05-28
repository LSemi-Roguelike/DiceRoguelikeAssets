using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class DiceUnit : ItemUnit
    {
        [SerializeField] protected DiceManager _diceManager;
        public DiceManager diceManager { get { return _diceManager; } }

        public override void Init()
        {
            _diceManager = Instantiate(_diceManager, transform);
            _diceManager.transform.localPosition = new Vector3(0, 2, 0);
            base.Init();
        }

        public void SetDiceManager(System.Action<List<UnitAction>> action)
        {
            diceManager.Init(this, weapon, action);
        }

        public void GetAction()
        {
            diceManager.GetActions(status.power);
        }
    }
}