using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseContainer : MonoBehaviour, IHaveInfo
    {
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        
        [SerializeField] protected BaseUnit _unit;          //��˫ë�
        [SerializeField] protected UnitStatusUI _statusUI;  //����?����

        public BaseUnit Unit => _unit;

        public uint ID => _id;

        public string Name => _name;

        public Vector3 Pos => transform.position;

        //��Ѣ��
        public virtual void Init()
        {
            _unit.Init(this);
            _statusUI.InitUI(_unit.MaxStatus);
            SetStatusUI();
        }

        //����?�������֪�?��
        public virtual void GetEffect(Effect effect)
        {
            _unit.GetEffect(effect);
            SetStatusUI();
        }

        //UI����
        protected virtual void SetStatusUI()
        {
            _statusUI.SetUI(_unit.TotalStatus);
        }
    }
}