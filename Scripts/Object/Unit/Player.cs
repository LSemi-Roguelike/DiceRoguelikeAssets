using System;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Player : DiceUnit
    {

        protected virtual void Awake()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
        }

        public BaseItem AddItem(UpgradeItem item, Parts.PartsType partsType)
        {
            switch (partsType)
            {
                case Parts.PartsType.ARM:
                    armParts.Upgrade(item);
                    break;
                case Parts.PartsType.LEG:
                    legParts.Upgrade(item);
                    break;
                case Parts.PartsType.BODY:
                    bodyParts.Upgrade(item);
                    break;
            }
            SetTotalStatus();
            return item;
        }

        protected override void SetTotalStatus()
        {
            base.SetTotalStatus();
        }

        public void ChangeParts(Parts newParts)
        {
            switch (newParts.GetPartsType())
            {
                case Parts.PartsType.ARM:
                    Destroy(armParts);
                    armParts = newParts;
                    break;
                case Parts.PartsType.LEG:
                    Destroy(legParts);
                    legParts = newParts;
                    break;
                case Parts.PartsType.BODY:
                    Destroy(bodyParts);
                    bodyParts = newParts;
                    break;
            }
        }
    }
}