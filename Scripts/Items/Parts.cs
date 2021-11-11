using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parts", menuName = "Items/Parts", order = 10)]
public class Parts : BaseItem
{
    public enum PartsType { ARM, LEG, BODY }

    [SerializeField] PartsType type;
    [SerializeField] Status status;
    [SerializeField] Status statusMagnify;
    [SerializeField] List<SkillBase> partsActions;
    [SerializeField] int maxCost;

    Unit owner;

    List<UpgradeItem> upgrades;
    Status itemStatus;
    Status buff;
    public Status GetStatus() { return (status + itemStatus) * statusMagnify * buff;  }
    public PartsType GetPartsType() { return type; }

    public void Init(Unit owner)
    {
        this.owner = owner;
        upgrades = new List<UpgradeItem>();
        itemStatus = new Status();
        buff = new Status();
    }

    public void Upgrade(UpgradeItem item)
    {
        upgrades.Add(item);
        itemStatus += item.GetStatus();
    }

    public List<SkillBase> GetSkills()
    {
        return partsActions;
    }

    public void RemoveItem(UpgradeItem item)
    {
        upgrades.Remove(item);
        itemStatus -= item.GetStatus();
    }

    public void OnDestroy()
    {
        foreach(UpgradeItem i in upgrades)
            Destroy(i);
    }
}
