using UnityEngine;
public enum DiceType { MOVEMENT, SKILL }

public class Dice : MonoBehaviour
{
    [SerializeField]
    int maxCost;
    [SerializeField]
    DiceType diceType;

    public DiceParts[] parts { get; private set; }

    public DiceType GetDiceType(){return diceType;}

    private void Awake()
    {
        SetSideSprite();
    }

    private void Update()
    {

    }

    void TestUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            SelectSide(0);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectSide(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectSide(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectSide(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectSide(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectSide(5);

    }

    public void SetSideSprite()
    {
        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = parts[i].GetSprite();
        }
    }

    public void RollEnd()
    {
        //DiceParts selected;
        for (int i = 0; i < 6; i++)
        {
            if (transform.GetChild(i).position.y > transform.position.y)
            {
                SelectSide(i);
            }
        }
    }

    void SelectSide(int num)
    {
        //owner.SelectDiceParts(parts[num]);
    }
}
