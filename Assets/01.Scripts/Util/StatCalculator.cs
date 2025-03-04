using UnityEngine;

public class StatCalculator : MonoBehaviour
{
    public static float CalculateModifiedDamage(float baseDamage)
    {
        float modifier = SkillManager.Instance.GetAllPassiveStat().attackBonus * 0.01f;
        return baseDamage * (1f + modifier);
    }
    public static float CalculateModifiedHP(float baseHP)
    {
        float modifier = SkillManager.Instance.GetAllPassiveStat().hpBonus * 0.01f;
        return baseHP * (1f + modifier);
    }
    public static float CalculateModifiedDrop(float baseDrop)
    {
        float modifier = SkillManager.Instance.GetAllPassiveStat().dropRangeBonus * 0.01f;
        return baseDrop * (1f + modifier);
    }
    public static float CalculateModifiedLuck(float baseLuck)
    {
        float modifier = SkillManager.Instance.GetAllPassiveStat().luckBonus * 0.01f;
        return baseLuck + modifier;
    }
    public static int CalculateModifiedExp(float baseExp)
    {
        float modifier = SkillManager.Instance.GetAllPassiveStat().expBonus * 0.01f;
        return Mathf.RoundToInt(baseExp * (1f + modifier));
    }
}
