using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemObject : MonoBehaviour
    {
        [SerializeField]
        private BaseItem _item;
        public BaseItem Item { get { return _item; } }

        private void Awake()
        {
            gameObject.tag = "Item";
            GetComponent<SpriteRenderer>().sprite = _item.sprite;
        }
    }
}