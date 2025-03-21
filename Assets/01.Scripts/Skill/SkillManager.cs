using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    private List<ActiveSkillRuntime> activeSkills = new List<ActiveSkillRuntime>();
    private List<PassiveSkillRuntime> passiveSkills = new List<PassiveSkillRuntime>();

    public void Reset()
    {
        activeSkills.Clear();
        passiveSkills.Clear();
    }
    public int GetLevel(SkillDataBase skillData)
    {
        int level = 0;
        level = activeSkills.Find(x => x.Data == skillData)?.Level ?? 0;
        if(level == 0)
        {
            level = passiveSkills.Find(x => x.Data == skillData)?.Level ?? 0;
        }
        return level;
    }
    public string GetNextLevelDescription(SkillDataBase skillData)
    {
        int level = GetLevel(skillData);

        return SkillDescriptionGenerator.GetDescription(skillData, ++level);
    }
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
    public void LearnSkill(SkillDataBase data)
    {
        if (data is ActiveSkillData activeData)
        {
            var existingActive = activeSkills.Find(s => s.Data == activeData);
            if (existingActive != null)
            {
                existingActive.LevelUp();
            }
            else
            {
                var newSkill = new ActiveSkillRuntime(activeData, BattleManager.Instance.GetPlayableCharacter().transform);
                activeSkills.Add(newSkill);
            }
        }
        else if (data is PassiveSkillData passiveData)
        {
            var existingPassive = passiveSkills.Find(s => s.Data == passiveData);
            if (existingPassive != null)
            {
                existingPassive.LevelUp();
            }
            else
            {
                var newSkill = new PassiveSkillRuntime(passiveData, BattleManager.Instance.GetPlayableCharacter().transform);
                passiveSkills.Add(newSkill);
            }
        }
    }
    public PassiveSkillLevelInfo GetAllPassiveStat()
    {
        PassiveSkillLevelInfo passiveStat = new PassiveSkillLevelInfo();
        foreach(var skill in passiveSkills)
        {
            passiveStat += skill.Data.levelInfos[skill.Level -1];
        }
        return passiveStat;
    }
}
