using UnityEngine;

[CreateAssetMenu(fileName = "SkillDiceParts", menuName = "Items/SkillDiceParts", order = 30)]
public class SkillDiceParts : DiceParts
{
    [SerializeField]
    SkillBase diceSkill = null;
    public SkillBase getDiceSkill { get { return diceSkill; } }
}
