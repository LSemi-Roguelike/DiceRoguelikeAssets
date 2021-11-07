using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceObject : MonoBehaviour
{
    Dice dice;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //RollEnd();
        }
    }

    public void SetSideSprite()
    {
        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = dice.sides[i].GetSprite();
        }
    }

    public void RollEnd()
    {
        //DiceParts selected;
        for (int i = 0; i < 6; i++)
        {
            if (transform.GetChild(i).position.y > transform.position.y)
            {
                Debug.Log(i);
            }
        }
    }
}
