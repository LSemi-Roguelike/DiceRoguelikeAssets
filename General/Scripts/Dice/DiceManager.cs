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

        private PlayerUnit owner;
        private Weapon weapon;
        private int power;
        private System.Action<List<MainSkill>> returnAction;

        public void Init(PlayerUnit owner, Weapon weaponAction, System.Action<List<MainSkill>> action)
        {
            this.owner = owner;
            this.weapon = weaponAction;
            returnAction = action;
            dices.ForEach((d) => d.Init(owner));
        }

        public void GetActions(int power)
        {
            this.power = power;
            DiceSelectUI.inst.SetDiceUI(dices, weapon, power, GetSelected);
        }

        public void GetSelected(bool[] diceUse, bool weaponUse)
        {
            if (diceUse == null && !weaponUse)
            {
                returnAction(new List<MainSkill>());
                return;
            }

            var useDice = new List<Dice>();
            for (int i = 0; i < diceUse.Length; i++)
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
            var results = new List<MainSkill>();

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
                results.Add(weapon.skills[0]);
            returnAction(results);
            foreach (var obj in diceObjects)
            {
                Destroy(obj.gameObject);
            }
        }
    }
}