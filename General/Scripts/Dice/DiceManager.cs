using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ramdom = UnityEngine.Random;

namespace LSemiRoguelike
{
    public class DiceManager : MonoBehaviour
    {
        [SerializeField] private List<Dice> dices;
        [SerializeField] private DiceObject dicePrefab;
        [SerializeField] private float height = 1, width = 5, throwTime = 1;

        private DiceUnit owner;
        private Weapon weapon;
        private int power;
        private System.Action<List<UnitAction>> returnAction;

        public void Init(DiceUnit owner, Weapon weaponAction, System.Action<List<UnitAction>> action)
        {
            this.owner = owner;
            this.weapon = weaponAction;
            returnAction = action;
        }

        public void GetActions(int power)
        {
            this.power = power;
            DiceSelectUI.inst.SetDiceUI(dices, weapon, power, GetSelected);
        }

        public void GetSelected(bool[] diceUse, bool weaponUse)
        {
            int cost = weaponUse ? 1 : 0;
            foreach (var use in diceUse)
                if (use) cost++;
            if (cost > power)
                return;

            DiceSelectUI.inst.Permit();

            var useDice = new List<Dice>();
            for (int i = 0; i < dices.Count; i++)
            {
                if (diceUse[i]) useDice.Add(dices[i]);
            }
            StartCoroutine(RollDice(useDice, weaponUse));
        }

        IEnumerator RollDice(List<Dice> useDice, bool weaponUse)
        {
            var diceObjects = new DiceObject[useDice.Count];
            for (int i = 0; i < useDice.Count; i++)
            {
                diceObjects[i] = Instantiate(dicePrefab, transform);
                diceObjects[i].gameObject.SetActive(false);
            }

            var count = diceObjects.Length;
            var interval = width / (count + 1);
            var results = new List<UnitAction>();

            for (int i = 0; i < count; i++)
            {
                diceObjects[i].gameObject.SetActive(true);
                diceObjects[i].transform.localPosition =
                    new Vector3(-width / 2 + interval * (i + 1), height, 0);
                StartCoroutine(diceObjects[i].RollDice(useDice[i], (p) => results.Add(p)));
                yield return new WaitForSeconds(throwTime / count);
            }

            yield return new WaitUntil(() => results.Count == useDice.Count);
            if (weaponUse)
                results.Add(weapon.unitAction);
            returnAction(results);
            foreach (var obj in diceObjects)
            {
                Destroy(obj.gameObject);
            }
        }
    }
}