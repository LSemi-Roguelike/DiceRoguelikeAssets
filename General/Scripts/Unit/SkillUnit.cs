using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class SkillUnit : ActingUnit
    {
        [SerializeField] List<MainSkill> mainSkillPrefabs;
        [SerializeField] List<SubSkill> attSubPrefabs, moveSubPrefabs, dmgSubPrefabs;
        [SerializeField] List<PassiveSkill> passivePrefabs;

        List<MainSkill> _mainSkills;
        List<SubSkill> _attSub, _moveSub, _dmgSub;
        List<PassiveSkill> _passive;

        Action<List<MainSkill>> actionCallBacck;

        protected override void Init()
        {
            _mainSkills = new List<MainSkill>();
            _attSub = new List<SubSkill>();
            _moveSub = new List<SubSkill>();
            _dmgSub = new List<SubSkill>();
            _passive = new List<PassiveSkill>();

            mainSkillPrefabs.ForEach((s) => _mainSkills.Add(Instantiate(s)));
            attSubPrefabs.ForEach((s) => _attSub.Add(Instantiate(s)));
            moveSubPrefabs.ForEach((s) => _moveSub.Add(Instantiate(s)));
            dmgSubPrefabs.ForEach((s) => _dmgSub.Add(Instantiate(s)));
            passivePrefabs.ForEach((s) => _passive.Add(Instantiate(s)));

            _mainSkills.ForEach((s) => s.Init(this));
            _attSub.ForEach((s) => s.Init(this));
            _moveSub.ForEach((s) => s.Init(this));
            _dmgSub.ForEach((s) => s.Init(this));
            _passive.ForEach((s) => s.Init(this));

            base.Init();
            Debug.Log(_passive.Count);
        }

        public override void Attack(float damage)
        {
            _attSub.ForEach((s) => s.SupplyPower(damage));
        }
        public override void Move(float movement)
        {
            _moveSub.ForEach((s) => s.SupplyPower(movement));
        }

        public override void Damaged(float damage)
        {
            _dmgSub.ForEach((s) => s.SupplyPower(damage));
        }

        public override void Passive()
        {
            _passive.ForEach((s) => s.Passive());
        }

        public override void GetAction()
        {
            actionCallBacck(_mainSkills);
        }

        public override void SetActionCallback(Action<List<MainSkill>> action)
        {
            actionCallBacck = action;
        }
    }
}