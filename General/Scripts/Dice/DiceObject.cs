using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;
using Ramdom = UnityEngine.Random;


public class DiceObject : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject up;
    [SerializeField] GameObject down;
    [SerializeField] GameObject forward;
    [SerializeField] GameObject back;
    [SerializeField] GameObject right;
    [SerializeField] GameObject left;
    [SerializeField] private float minPower, maxPower, throwPower;

    GameObject[] sides;
    SpriteRenderer[] sideSprites;
    Coroutine coroutine;

    float randValue => Random.Range(minPower, maxPower);
    float xPos, zPos;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sides = new GameObject[] { right, up, forward, left, down, back };
        sideSprites = new SpriteRenderer[sides.Length];
        for (int i = 0; i < sides.Length; i++)
        {
            sideSprites[i] = sides[i].GetComponent<SpriteRenderer>();
        }
    }

    public IEnumerator RollDice(Dice dice, System.Action<MainSkill> partsReturn)
    {
        xPos = transform.position.x;
        zPos = transform.position.z;
        for (int i = 0; i < sides.Length; i++)
        {
            sideSprites[i].sprite = dice.Skills[i].sprite;
        }
        coroutine = StartCoroutine(RollCo());
        yield return coroutine;

        var rot = transform.InverseTransformDirection(Vector3.up);
        var num = (int)Mathf.Round(rot.x + rot.y * 2 + rot.z * 3);
        num = num > 0 ? num - 1 : 2 - num;
        partsReturn(dice.Skills[num%6]);
    }
    
    IEnumerator RollCo()
    {
        transform.rotation = Quaternion.Euler(Ramdom.Range(0, 360), Ramdom.Range(0, 360), Ramdom.Range(0, 360));
        rb.velocity = Vector3.up * throwPower;
        rb.angularVelocity = new Vector3(randValue, randValue, randValue);
        while (rb.velocity.magnitude > 0.01f || rb.angularVelocity.magnitude > 0.01f)
        {
            transform.position = new Vector3(xPos, transform.position.y, zPos);
            yield return null;
        }
        yield break;
    }
}
