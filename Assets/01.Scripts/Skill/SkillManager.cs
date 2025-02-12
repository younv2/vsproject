using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    private List<SkillRuntime> activeSkills = new List<SkillRuntime>();

    public void ManualUpdate()
    {
        float dt = Time.deltaTime;
        foreach (var skill in activeSkills)
        {
            skill.Update(dt);
        }
    }

    /// <summary>
    /// 스킬을 새로 습득하거나 레벨업한다
    /// </summary>
    public void LearnSkill(SkillData data)
    {
        // 이미 같은 SkillData를 가진 스킬이 있으면 레벨업
        var existing = activeSkills.Find(s => s.Data == data);
        if (existing != null)
        {
            existing.LevelUp();
        }
        else
        {
            // 없으면 새로 추가
            var newSkill = new SkillRuntime(data, this.transform);
            activeSkills.Add(newSkill);
        }
    }
}
