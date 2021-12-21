using System;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class ActorUnit : BaseUnit, IHaveInfo
    {
        [SerializeField] protected Status statusMagnify = Status.one;
        protected int turnPoint = 3;

        protected Status buffStatus;

        public override void Init()
        {
            base.Init();
            turnPoint = Utils.StdTurnPoint;
        }

        public int GetTurnPoint()
        {
            return turnPoint;
        }

        protected override void SetTotalStatus()
        {
            totalStatus = new Status();
            totalStatus += unitStatus;

            totalStatus *= statusMagnify;

            totalStatus += buffStatus;
        }
    }
}