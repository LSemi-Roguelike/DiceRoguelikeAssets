using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Chip : MonoBehaviour, IHaveInfo
    {
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        public uint ID { get { return _id; } }
        public string Name { get { return _name; } }

        [SerializeField] protected Status _status;
        [SerializeField] protected SubSkill skillPrefab;
        [SerializeField] protected float _cost;
        protected SubSkill _skill;
        protected float _nowpower;
        protected Parts parts;

        public Chip Init(Parts parts)
        {
            this.parts = parts;
            _nowpower = 0;
            _skill = Instantiate(skillPrefab, transform);
            return this;
        }

        public IEnumerator SupplyPower(float power)
        {
            _nowpower += power;
            if (_nowpower > _cost)
            {
                yield return StartCoroutine(_skill.Cast(parts.owner.container, _nowpower - (_nowpower % _cost)));
                _nowpower -= _cost;
            }
            yield return null;
        }
    }
}
