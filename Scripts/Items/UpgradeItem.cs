using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Items/Item", order = 0)]
public class UpgradeItem : BaseItem
{
    [SerializeField]
    string itemID;
    [SerializeField]
    string itemName;
    [SerializeField]
    Status status;

    protected Parts parts;

    public string GetItemID() { return itemID; }
    public string GetItemName() { return itemName; }
    public Status GetStatus() { return status; }

    public virtual UpgradeItem Init(UpgradeItem item, Parts parts)
    {
        this.parts = parts;
        this.itemID = item.itemID;
        this.itemName = item.itemName;
        this.sprite = item.sprite;
        return this;
    }

    public virtual UpgradeItem Init(UpgradeItem item)
    {
        this.parts = null;
        this.itemID = item.itemID;
        this.itemName = item.itemName;
        this.sprite = item.sprite;
        return this;
    }

    public void SetOwner(Parts owner){ this.parts = owner; }

    public virtual void Destroy()
    {
        if(parts != null)
            parts.RemoveItem(this);
        Destroy(this);
    }
}
