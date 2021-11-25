using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceManager : MonoBehaviour
{
    [SerializeField] protected List<Dice> moveDices;
    [SerializeField] protected List<Dice> skillDices;

    public abstract IEnumerator SelectMoveDice(System.Action<Movement> action);
    public abstract IEnumerator SelectSkillDice(System.Action<Skill> action);
}
