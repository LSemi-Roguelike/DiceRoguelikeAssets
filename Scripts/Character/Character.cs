using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    string id;
    [SerializeField]
    string charName;

    [SerializeField]
    protected Status charStatus;
    protected Status perStatus;
    protected List<Status> itemStatus;
    protected List<Status> buffStatus;
    protected List<Status> buffPerStatus;

    protected Status totalStatus;

    protected Dictionary<ItemType, List<Item>> items;
    protected List<Action> Actions;
    protected Dictionary<DiceType, List<Dice>> dices;

    [SerializeField]
    protected SkillBase attack;
    protected List<SkillBase> Skills;

    protected Weapon weapon;
    protected Parts arm, leg, armor;

    public string GetName() { return charName; }
    public string GetID() { return id; }

    protected virtual void Awake()
    {
        itemStatus = new List<Status>();
        items = new Dictionary<ItemType, List<Item>>();
        foreach (ItemType itemType in System.Enum.GetValues(typeof(ItemType)))
        {
            items[itemType] = new List<Item>();
        }

        dices = new Dictionary<DiceType, List<Dice>>();
        foreach (DiceType diceType in System.Enum.GetValues(typeof(DiceType)))
        {
            dices[diceType] = new List<Dice>();
        }
        perStatus = Status.one;
        SetTotalStatus();
    }

    public Item AddItem(Item item)
    {
        items[item.GetItemType()].Add(item);
        item.SetOwner(this);
        switch(item.GetItemType())
        {
            case ItemType.DICEPARTS:
                break;
            case ItemType.PARTS:
                break;
            case ItemType.PASSIVE:
                break;
            case ItemType.WEAPONE:
                weapon = item as Weapon;
                attack = weapon.GetAttackAct();
                break;
        }
        if (item.GetHaveStatus())
        {
            itemStatus.Add(item.GetStatus());
            SetTotalStatus();
        }
        return item;
    }

    protected void SetTotalStatus()
    {
        totalStatus = new Status();

        totalStatus += charStatus;
        foreach (Status sta in itemStatus)
        {
            totalStatus += sta;
        }

        totalStatus *= perStatus;
    }

    public void RemoveItem(Item item)
    {
        items[item.GetItemType()].Remove(item);

        if (item.GetHaveStatus())
        {
            itemStatus.Remove(item.GetStatus());
            SetTotalStatus();
        }
    }
}