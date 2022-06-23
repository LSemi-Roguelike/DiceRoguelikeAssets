using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class PlayerUnit : ActingUnit
    {
        [SerializeField] protected DiceManager _diceManager;
        [SerializeField] protected int maxPower;

        [Header("Items")]
        [SerializeField] protected Weapon weaponPrefab;
        [SerializeField] protected Parts armPartsPrefab, legPartsPrefab, bodyPartsPrefab;
        [SerializeField] protected List<Accessory> accessoryPrefabs;

        protected int power;

        protected Weapon weapon;
        protected Parts armParts, legParts, bodyParts;
        protected List<Accessory> accessorys;
        protected Status buffStatus;
        public DiceManager diceManager { get { return _diceManager; } }

        protected override void Init()
        {
            _diceManager = Instantiate(_diceManager, transform);
            (weapon = Instantiate(weaponPrefab, transform)).Init(this);
            (armParts = Instantiate(armPartsPrefab, transform)).Init(this);
            (legParts = Instantiate(legPartsPrefab, transform)).Init(this);
            (bodyParts = Instantiate(bodyPartsPrefab, transform)).Init(this);
            accessorys = new List<Accessory>();
            accessoryPrefabs.ForEach((a) =>
            {
                accessorys.Insert(0, Instantiate(a, transform));
                accessorys[0].Init(this);
            });

            _diceManager.transform.localPosition = new Vector3(0, 2, 0);
            power = maxPower;
            base.Init();
        }

        public override void Attack()
        {
            armParts.PowerGenerate();
        }

        public override void Move()
        {
            legParts.PowerGenerate();
        }

        protected override void Damaged()
        {
            bodyParts.PowerGenerate();
        }
        public override void Passive()
        {
            foreach (var accessory in accessorys)
            {
                accessory.Passive();
            }
        }
        public override void SetActionCallback(System.Action<List<MainSkill>> action)
        {
            diceManager.Init(this, weapon, action);
        }

        public override void GetSkill()
        {
            diceManager.GetActions(power);
        }
    }
}