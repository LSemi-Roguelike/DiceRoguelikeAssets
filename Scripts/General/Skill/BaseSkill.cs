using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseSkill : MonoBehaviour
    {
        [SerializeField] protected int _grade = 0;
        public int grade { get { return _grade; } }
    }
}