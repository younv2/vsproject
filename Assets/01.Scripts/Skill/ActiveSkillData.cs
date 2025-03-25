using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/ActiveSkillData")]
public class ActiveSkillData : SkillDataBase
{
    [Header("레벨별 설정")]
    public List<ActiveSkillLevelInfo> levelInfos;

    public float GetCooldown(int level)
    {
        int idx = Mathf.Clamp(level - 1, 0, levelInfos.Count - 1);
        return levelInfos[idx].cooldown;
    }

    public void Activate(ActiveSkillRuntime runtime)
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
