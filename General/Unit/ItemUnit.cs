using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class ItemUnit : BaseUnit
    {
        [SerializeField] protected Weapon weaponPrefab;
        [SerializeField] protected Parts armPartsPrefab, legPartsPrefab, bodyPartsPrefab;
        [SerializeField] protected List<Accessory> accessoryPrefabs;
        protected Weapon weapon;
        protected Parts armParts, legParts, bodyParts;
        protected List<Accessory> accessorys;

        protected Status buffStatus;

        public override void Init()
        {
            weapon = Instantiate(weaponPrefab, transform).Init(this);
            armParts = Instantiate(armPartsPrefab, transform).Init(this);
            legParts = Instantiate(legPartsPrefab, transform).Init(this);
            bodyParts = Instantiate(bodyPartsPrefab, transform).Init(this);

            accessorys = new List<Accessory>();
            foreach (Accessory accessory in accessoryPrefabs)
            {
                accessorys.Add(Instantiate(accessory, transform).Init());
            }
            base.Init();
        }


        public IEnumerator Attack(float power)
        {
            yield return StartCoroutine(armParts.PowerGenerate(power));
        }

        public IEnumerator Move(float power)
        {
            yield return StartCoroutine(legParts.PowerGenerate(power));
        }

        public IEnumerator GetEffectCo(Effect effect)
        {
            GetEffect(effect);
            if(IsDead) yield break;

            if(effect.status.hp < 0)
                yield return StartCoroutine(bodyParts.PowerGenerate(effect.status.hp));
        }
    }
}