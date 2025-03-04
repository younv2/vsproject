using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ActiveSkillLevelInfo
{
    public float cooldown = 1f;              // 해당 레벨의 쿨타임
    public float baseDamage = 10f;
    public int projectileCount = 1;
    public float angleBetweenProjectiles = 10f;
    public List<SkillModule> levelModules;   // 해당 레벨에서 실행될 모듈 목록
}
[System.Serializable]
public class PassiveSkillLevelInfo
{
    public float attackBonus;
    public float hpBonus;
    public float dropRangeBonus;
    public float expBonus;
    public float luckBonus;

    public PassiveSkillLevelInfo(float attackBonus, float hpBonus, float dropRangeBonus, float expBonus, float luckBonus)
    {
        this.attackBonus = attackBonus;
        this.hpBonus = hpBonus;
        this.dropRangeBonus = dropRangeBonus;
        this.expBonus = expBonus;
        this.luckBonus = luckBonus;
    }
    public static PassiveSkillLevelInfo operator +(PassiveSkillLevelInfo data1, PassiveSkillLevelInfo data2)
    {
        return new PassiveSkillLevelInfo(data1.attackBonus + data2.attackBonus,
            data1.hpBonus + data2.hpBonus,
            data1.dropRangeBonus + data2.dropRangeBonus,
            data1.expBonus + data2.expBonus,
            data1.luckBonus + data2.luckBonus);
    }
}
public class SkillDataBase : ScriptableObject
{
    public int skillId;
    public string skillName;

}
