using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseItem<T> : MonoBehaviour, IHaveInfo where T : BaseSkill
    {
        //info
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        [SerializeField] protected Sprite _sprite;

        [SerializeField] protected Ability _ability;
        [SerializeField] protected Status _status;

        [SerializeField] int socketCount;
        [SerializeField] T[] skillPrefabs;

        protected T[] _skills;
        protected PlayerUnit _owner;
        protected Renderer _renderer;

        public Status status => status;
        public T[] skills => _skills;
        protected void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.enabled = false;
        }

        public virtual void Init(PlayerUnit owner)
        {
            _owner = owner;
            _skills = new T[socketCount];
            for (int i = 0; i < (skillPrefabs.Length > socketCount ? socketCount : skillPrefabs.Length); i++)
            {
                skills[i] = Instantiate(skillPrefabs[i], transform);
                skills[i].Init(owner);
            }
        }

        public uint ID => _id;
        public string Name => _name;
        public Sprite sprite => _sprite;
        public PlayerUnit owner => _owner;
    }
}