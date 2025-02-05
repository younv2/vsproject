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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
