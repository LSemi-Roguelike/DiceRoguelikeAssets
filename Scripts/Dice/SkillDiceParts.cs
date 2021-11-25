using UnityEngine;

[CreateAssetMenu(fileName = "SkillDiceParts", menuName = "Items/SkillDiceParts", order = 30)]
public class SkillDiceParts : DiceParts
{
    [SerializeField]
    Skill diceSkill = null;
    public Skill getDiceSkill { get { return diceSkill; } }
}
