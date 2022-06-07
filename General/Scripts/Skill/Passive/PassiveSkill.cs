using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class PassiveSkill : BaseSkill
    {
        [SerializeField] private int _cost;
        private int nowTurn;

        public void GetTurn()
        {
            nowTurn++;
            if (nowTurn == _cost)
            {
                StartCoroutine(Cast());
                nowTurn = 0;
            }
        }

        protected abstract IEnumerator Cast();
    }
}