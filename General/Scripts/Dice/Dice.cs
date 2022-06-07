using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class Dice
    {
        [SerializeField] private int _maxCost;
        [SerializeField] private MainSkill up, down, left, right, forward, back;
        private MainSkill[] _skills;

        public int MaxCost => _maxCost;
        public MainSkill[] Skills => _skills;

        public void Init(PlayerUnit caster)
        {
            var obj = new GameObject("Dice").transform;
            obj.parent = caster.transform;
            _skills = new MainSkill[6] {
                GameObject.Instantiate(right, obj),
                GameObject.Instantiate(up, obj),
                GameObject.Instantiate(forward, obj),
                GameObject.Instantiate(left, obj),
                GameObject.Instantiate(down, obj),
                GameObject.Instantiate(back, obj)
            };

            for (int i = 0; i < 6; i++)
                _skills[i].Init(caster);
        }

        public override string ToString()
        {
            return $"{Skills}";
        }
    }
}