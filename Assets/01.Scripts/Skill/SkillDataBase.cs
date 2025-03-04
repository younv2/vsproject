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
}
public class SkillDataBase : ScriptableObject
{
    public int skillId;
    public string skillName;

}
