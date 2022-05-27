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
        [SerializeField] private GameObject plank;
        [SerializeField] private float height = 1, width = 5, throwTime = 1;

        private DiceUnit owner;
        private Weapon weapon;
        private int power;
        private bool weaponUse;
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
            DiceSelectUI.inst.SetDiceUI(dices, weapon, GetSelected);
        }

        public IEnumerator RollDices()
        {
            var numKeys = new KeyCode[]
            {
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
                KeyCode.Alpha4,
                KeyCode.Alpha5,
                KeyCode.Alpha6,
                KeyCode.Alpha7,
                KeyCode.Alpha8,
                KeyCode.Alpha9,
                KeyCode.Alpha0,
            };
            var powerCost = 0;
            var diceUse = new bool[dices.Count];
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    returnAction(new List<UnitAction>());
                    yield break;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    weaponUse = !weaponUse;
                    powerCost += weaponUse ? 1 : -1;
                    Debug.Log("weapon use: " + weaponUse);
                    Debug.Log("Power cost: " + powerCost + "/" + power);
                }
                for (int i = 0; i < dices.Count; i++)
                {
                    if (Input.GetKeyDown(numKeys[i]))
                    {
                        diceUse[i] = !diceUse[i];
                        powerCost += diceUse[i] ? 1 : -1;
                        Debug.Log("dice " + i + " use: " + diceUse[i]);
                        Debug.Log("Power cost: " + powerCost + "/" + power);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("Power cost: " + powerCost + "/" + power);
                    if (powerCost > power)
                    {
                        Debug.Log("Cost over");
                    }
                    else
                    {
                        break;
                    }
                }
                yield return null;
            }

            var useDice = new List<Dice>();
            for (int i = 0; i < dices.Count; i++)
            {
                if (diceUse[i]) useDice.Add(dices[i]);
            }

            var count = useDice.Count;
            var diceObjects = new DiceObject[count];
            for (int i = 0; i < count; i++)
            {
                diceObjects[i] = Instantiate(dicePrefab, transform);
                diceObjects[i].gameObject.SetActive(false);
            }
            plank.SetActive(false);


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

        public void GetSelected(bool[] diceUse, bool weaponeUse)
        {
            int cost = weaponeUse ? 1 : 0;
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
            StartCoroutine(RollDice(useDice));
        }

        IEnumerator RollDice(List<Dice> useDice)
        {
            var diceObjects = new DiceObject[useDice.Count];
            for (int i = 0; i < useDice.Count; i++)
            {
                diceObjects[i] = Instantiate(dicePrefab, transform);
                diceObjects[i].gameObject.SetActive(false);
            }

            plank.SetActive(false);
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