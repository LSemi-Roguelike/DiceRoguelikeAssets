using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;
public class DiceThrower : MonoBehaviour
{
    public DiceObject prefab;
    [SerializeField] private float minPower, maxPower;
    [SerializeField] private float throwPower;
    [SerializeField] private float height = 1;
    [SerializeField] private float width = 5;
    [SerializeField] private float throwTime = 1;
    [SerializeField] private int count = 3;
    [SerializeField] private GameObject plank;

    [SerializeField] private Dice testDice;
    private void Start()
    {

    }

    public IEnumerator ThrowDice(Dice[] dice, System.Action<List<UnitAction>> action)
    {
        var dices = new DiceObject[count];
        for (int i = 0; i < count; i++)
        {
            dices[i] = Instantiate(prefab, transform);
            dices[i].gameObject.SetActive(false);
        }
        plank.SetActive(false);


        var interval = width / (count + 1);
        var results = new List<UnitAction>();
        for (int i = 0; i < count; i++)
        {
            dices[i].gameObject.SetActive(true);
            dices[i].transform.localPosition = new Vector3(-width / 2 + interval * (i + 1), height, 0);
            StartCoroutine(dices[i].RollDice(dice[i], (p) =>results.Add(p)));
            yield return new WaitForSeconds(throwTime / count);
        }

        plank.transform.localPosition = new Vector3(0, height, 0);
        plank.GetComponent<Rigidbody>().velocity = Vector3.zero;
        plank.SetActive(true);

        yield return new WaitUntil(()=>results.Count == dice.Length);
        action(results);
    }
}
