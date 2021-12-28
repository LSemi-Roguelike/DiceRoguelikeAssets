using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class DiceManager : MonoBehaviour
    {
        [SerializeField] protected List<Dice> moveDices;
        [SerializeField] protected List<Dice> skillDices;

        public abstract IEnumerator SelectMoveDice(System.Action<int> action);
        public abstract IEnumerator SelectSkillDice(System.Action<MainSkill> action);
    }
}