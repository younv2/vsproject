using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    public void UseSkill(Skill skill)
    {
        Debug.Log($"{skill.SkillData.SkillName}¸¦ »ç¿ë");
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
