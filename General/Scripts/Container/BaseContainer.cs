using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseContainer : MonoBehaviour, IHaveInfo
    {
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        
        [SerializeField] protected BaseUnit _unit;          //«æ«Ë«Ã«È
        [SerializeField] protected UnitStatusUI _statusUI;  //«¹«Æ?«¿«¹

        public BaseUnit Unit => _unit;

        public uint ID => _id;

        public string Name => _name;

        public Vector3 Pos => transform.position;

        //ôøÑ¢ûù
        public virtual void Init()
        {
            _unit.Init(this);
            _statusUI.InitUI(_unit.MaxStatus);
            SetStatusUI();
        }

        //«À«á?«¸£üüŞÜÖªÎ?×â
        public virtual void GetEffect(Effect effect)
        {
            _unit.GetEffect(effect);
            SetStatusUI();
        }

        //UIÌÚãæ
        protected virtual void SetStatusUI()
        {
            _statusUI.SetUI(_unit.TotalStatus);
        }
    }
}