using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseSkill : MonoBehaviour
    {
        [SerializeField] protected int _grade = 0;
        [SerializeField] protected Sprite _sprite;
        protected ActingUnit _caster;
        protected BaseContainer _container => _caster.Container;
        public int grade => _grade;
        public Sprite sprite => _sprite;
        public ActingUnit caster => _caster;
        public virtual void Init(ActingUnit caster) { _caster = caster; }
    }
}