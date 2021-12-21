using System;
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
            base.Init();
        }

        protected override void SetTotalStatus()
        {
            totalStatus = new Status();

            totalStatus += unitStatus;

            totalStatus += armParts.GetStatus();
            totalStatus += legParts.GetStatus();
            totalStatus += bodyParts.GetStatus();
            totalStatus += weapon.GetStatus();

            totalStatus *= statusMagnify;
        }
    }
}