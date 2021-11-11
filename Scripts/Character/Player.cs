using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    protected List<BaseItem> items;
    protected Dictionary<DiceType, List<Dice>> dices;



    protected virtual void Awake()
    {
        items = new List<BaseItem>();

        dices = new Dictionary<DiceType, List<Dice>>();
        foreach (DiceType diceType in Enum.GetValues(typeof(DiceType)))
        {
            dices[diceType] = new List<Dice>();
        }
        SetTotalStatus();
    }

    public BaseItem AddItem(UpgradeItem item, Parts.PartsType partsType)
    {
        switch(partsType)
        {
            case Parts.PartsType.ARM:
                armParts.Upgrade(item);
                break;
            case Parts.PartsType.LEG:
                legParts.Upgrade(item);
                break;
            case Parts.PartsType.BODY:
                bodyParts.Upgrade(item);
                break;
        }
        SetTotalStatus();
        return item;
    }

    protected override void SetTotalStatus()
    {
        totalStatus = new Status();

        totalStatus += charStatus;
    }

    public void ChangeParts(Parts newParts)
    {
        switch (newParts.GetPartsType())
        {
            case Parts.PartsType.ARM:
                Destroy(armParts);
                armParts = newParts;
                break;
            case Parts.PartsType.LEG:
                Destroy(legParts);
                legParts = newParts;
                break;
            case Parts.PartsType.BODY:
                Destroy(bodyParts);
                bodyParts = newParts;
                break;
        }
    }
}
