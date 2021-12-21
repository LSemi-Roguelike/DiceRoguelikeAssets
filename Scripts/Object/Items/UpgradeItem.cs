using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "UpgradeItem", menuName = "Items/UpgradeItem", order = 30)]
    public class UpgradeItem : BaseItem
    {
        [SerializeField]
        Status status;

        protected Parts parts;
        public Status GetStatus() { return status; }

        public virtual UpgradeItem Init(UpgradeItem item, Parts parts)
        {
            this.parts = parts;
            this._owner = item._owner;
            return this;
        }

        public virtual UpgradeItem Init(UpgradeItem item)
        {
            this.parts = null;
            this._owner = item._owner;
            return this;
        }

        public void SetOwner(Parts owner) { this.parts = owner; }

        public virtual void Destroy()
        {
            if (parts != null)
                parts.RemoveItem(this);
            Destroy(this);
        }
    }
}