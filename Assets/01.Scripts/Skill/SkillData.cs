using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SkillLevelInfo
{
    public float cooldown = 1f;              // 해당 레벨의 쿨타임
    public float baseDamage = 10f;
    public int projectileCount = 1;
    public float angleBetweenProjectiles = 10f;
    public List<SkillModule> levelModules;   // 해당 레벨에서 실행될 모듈 목록
}

[CreateAssetMenu(menuName = "Game/SkillData")]
public class SkillData : ScriptableObject
{
    public int skillId;
    public string skillName;

    [Header("레벨별 설정")]
    public List<SkillLevelInfo> levelInfos;

    /// <summary>
    /// 특정 레벨에 맞는 쿨다운 조회
    /// </summary>
    public float GetCooldown(int level)
    {
        // 레벨 -1을 인덱스로, 범위 초과 시 마지막 레벨 사용
        int idx = Mathf.Clamp(level - 1, 0, levelInfos.Count - 1);
        return levelInfos[idx].cooldown;
    }

    /// <summary>
    /// 스킬이 발동될 때, 현재 레벨의 이펙트를 실행
    /// </summary>
    public void Activate(SkillRuntime runtime)
    {
        int idx = Mathf.Clamp(runtime.Level - 1, 0, levelInfos.Count - 1);
        var modules = levelInfos[idx].levelModules;
        if (modules == null) return;

        foreach (var module in modules)
        {
            module.Execute(runtime);
        }
    }
}