using UnityEngine;
public enum ItemType { PASSIVE, ACTIVE, DICE, DICEPARTS, PARTS, WEAPONE }

[CreateAssetMenu(fileName ="Item", menuName ="Items/Item", order = 0)]
public class Item : ScriptableObject
{
    [SerializeField]
    string itemID;
    [SerializeField]
    string itemName;
    [SerializeField]
    bool haveStatus;
    [SerializeField]
    Sprite sprite;  
    [SerializeField]
    Status status;

    protected Character owner;
    protected ItemType itemType;

    public string GetItemID() { return itemID; }
    public string GetItemName() { return itemName; }
    public ItemType GetItemType() { return itemType; }
    public bool GetHaveStatus() { return haveStatus; }
    public Sprite GetSprite() { return sprite; }
    public Status GetStatus() { return status; }

    private void OnEnable()
    {
        itemType = ItemType.PASSIVE;
    }

    private void OnValidate()
    {
        //Debug.Log("a");
    }

    public virtual Item Init(Item item, Character owner)
    {
        this.owner = owner;
        this.itemID = item.itemID;
        this.itemName = item.itemName;
        this.itemType = item.itemType;
        this.sprite = item.sprite;
        return this;
    }

    public virtual Item Init(Item item)
    {
        this.owner = null;
        this.itemID = item.itemID;
        this.itemName = item.itemName;
        this.itemType = item.itemType;
        this.sprite = item.sprite;
        return this;
    }

    public void SetOwner(Character owner){ this.owner = owner; }

    public virtual void Destroy()
    {
        if(owner != null)
            owner.RemoveItem(this);
        Destroy(this);
    }
}
