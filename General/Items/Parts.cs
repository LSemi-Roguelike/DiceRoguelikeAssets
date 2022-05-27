using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class Parts : BaseItem
    {
        public enum PartsType { ARM, LEG, BODY }

        [SerializeField] protected PartsType type;
        [SerializeField] protected Chip[] chipPrefabs;
        [SerializeField] protected int socketCount;
        [SerializeField] protected float powerGenMulti;
        [SerializeField] Ability ability;
        [SerializeField] Status itemStatus;
        ItemUnit _unit;
        protected Chip[] chips;

        public PartsType GetPartsType() { return type; }

        public Parts Init(ItemUnit unit)
        {
            _unit = unit;
            chips = new Chip[socketCount];
            for (int i = 0; i < (chipPrefabs.Length > socketCount? socketCount : chipPrefabs.Length); i++)
            {
                chips[i] = Instantiate(chipPrefabs[i], transform).Init(this);
            }
            return this;
        }


        public IEnumerator PowerGenerate(float power)
        {
            foreach (var chip in chips)
            {
                yield return StartCoroutine(chip.SupplyPower(power * powerGenMulti));
            }
        }

    }
}