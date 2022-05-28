using UnityEngine;

namespace LSemiRoguelike
{
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
            Destroy(this);
        }
    }
}