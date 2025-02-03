using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    public Skill GetSkill(string skillName)
    {
        return new Skill(skillName); 
    }

    public Skill GetSkill(int skillId)
    {
        return new Skill(skillId);
    }

    public void UseSkill(Skill skill)
    {
        Debug.Log($"{skill.GetSkillData().SkillName}¸¦ »ç¿ë");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
