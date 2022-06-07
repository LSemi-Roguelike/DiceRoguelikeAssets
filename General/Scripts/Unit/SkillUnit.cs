using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class SkillUnit : ActingUnit
    {
        [SerializeField] List<MainSkill> _unitActions;
        [SerializeField] List<SubSkill> _attSub, _moveSub, _dmgSub;
        [SerializeField] List<PassiveSkill> _passive;
        Action<List<MainSkill>> actionCallBacck;
        public override void Attack(float damage)
        {
            _attSub.ForEach((s) => s.SupplyPower(damage));
        }
        public override void Move(float movement)
        {
            _attSub.ForEach((s) => s.SupplyPower(movement));
        }


        public override void Damaged(float damage)
        {
            _attSub.ForEach((s) => s.SupplyPower(damage));
        }

        public override void GetAction()
        {
            actionCallBacck(_unitActions);
        }

        public override void SetActionCallback(Action<List<MainSkill>> action)
        {
            actionCallBacck = action;
        }
    }
}