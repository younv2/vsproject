using UnityEngine;

public class StatCalculator
{
    public static float CalculateModifiedDamage(PassiveSkillLevelInfo passiveSkillLevelInfo, float baseDamage)
    {
        float modifier = passiveSkillLevelInfo.attackBonus * 0.01f;
        return baseDamage * (1f + modifier);
    }
    public static float CalculateModifiedHP(PassiveSkillLevelInfo passiveSkillLevelInfo, float baseHP)
    {
        float modifier = passiveSkillLevelInfo.hpBonus * 0.01f;
        return baseHP * (1f + modifier);
    }
    public static float CalculateModifiedDrop(PassiveSkillLevelInfo passiveSkillLevelInfo, float baseDrop)
    {
        float modifier = passiveSkillLevelInfo.dropRangeBonus * 0.01f;
        return baseDrop * (1f + modifier);
    }
    public static float CalculateModifiedLuck(PassiveSkillLevelInfo passiveSkillLevelInfo, float baseLuck)
    {
        float modifier = passiveSkillLevelInfo.luckBonus * 0.01f;
        return baseLuck + modifier;
    }
    public static int CalculateModifiedExp(PassiveSkillLevelInfo passiveSkillLevelInfo, float baseExp)
    {
        float modifier = passiveSkillLevelInfo.expBonus * 0.01f;
        return Mathf.RoundToInt(baseExp * (1f + modifier));
    }
}
