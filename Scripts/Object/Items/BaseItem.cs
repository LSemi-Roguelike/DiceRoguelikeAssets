using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseItem : ScriptableObject, IHaveInfo
    {
        //info
        [SerializeField] protected string _id;
        [SerializeField] protected string _name;
        [SerializeField] protected Sprite _sprite;
        protected BaseUnit _owner;


        public string ID { get { return _id; } }
        public string Name { get { return _name; } }
        public Sprite sprite { get { return _sprite; } }
        public BaseUnit owner
        {
            get { return _owner; }
            set { if (!_owner) _owner = value; }
        }
        public virtual void Init()
        {

        }
    }
}