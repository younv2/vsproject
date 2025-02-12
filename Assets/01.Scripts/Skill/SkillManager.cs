using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    private List<SkillLevel> skillLevel;
    public List<SkillLevel> SkillLevel { get { return skillLevel; } }
    public void UseSkill(Skill skill)
    {
        Debug.Log($"{skill.SkillData.SkillName}를 사용");
    }
    public void GetSkill(int skillId)
    {
        DataManager.Instance.SkillDataList.Find(x => x.SkillId == skillId); 
    }
    public void GetSkill(string skillName)
    {
        DataManager.Instance.SkillDataList.Find(x => x.SkillName == skillName);
    }

    public void ManualUpdate()
    {
        
    }
}
